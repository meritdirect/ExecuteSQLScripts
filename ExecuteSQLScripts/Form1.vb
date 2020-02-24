Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports ListViewEmbeddedControls.ListViewEx
Imports System.ComponentModel
Imports ScintillaNET

'Install-Package WindowsAPICodePack-Shell


Public Class Form1
    Private rptFile As String
    Private strSQLPath As String = String.Empty, strServer As String = String.Empty
    Dim WithEvents conn As SqlConnection
    Dim currentThread As Threading.Thread
    Dim bAbort As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' FolderBrowserDialog1.ShowDialog()
        Dim dlg As New CommonOpenFileDialog
        dlg.IsFolderPicker = True

        If dlg.ShowDialog = CommonFileDialogResult.Ok Then
            strSQLPath = dlg.FileName
            txtFolder.Text = strSQLPath
            getFiles(strSQLPath)

        Else
            Exit Sub
        End If



    End Sub
    Private Function LogMessages(ByVal sender As Object, ByVal e As SqlInfoMessageEventArgs) As Boolean
        File.AppendAllText(rptFile, e.Message)
        Using sr As StreamWriter = File.AppendText(rptFile)
            sr.WriteLine()
        End Using
        Return True
    End Function
    Private Sub OnStatementCompleted(sender As Object, args As StatementCompletedEventArgs)
        Dim msg As String = String.Format("({0} row(s) affected)", args.RecordCount)
        'Write msg to output
        File.AppendAllText(rptFile, msg)
        Using sr As StreamWriter = File.AppendText(rptFile)
            sr.WriteLine()
        End Using
    End Sub
    Private Sub RunScripts()

        Dim strConnectionString As String = String.Empty
        strServer = txtServer.Text
        strConnectionString = My.Settings.ConnectionString.Replace("@Server", strServer)

        Using conn = New SqlClient.SqlConnection(strConnectionString)
            conn.FireInfoMessageEventOnUserErrors = True
            Try
                conn.Open()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                Exit Sub
            End Try
            AddHandler conn.InfoMessage, Function(sender1, e1) LogMessages(sender1, e1)
            For Each ViewItem As ListViewItem In clbScripts.CheckedItems
                'listView1.GetEmbeddedControl(1, row)
                Dim pb As New PictureBox
                pb.SizeMode = PictureBoxSizeMode.Zoom
                pb.Image = My.Resources.waiting
                clbScripts.AddEmbeddedControl(pb, 1, ViewItem.Index, DockStyle.Fill)
                clbScripts.Refresh()
                rptFile = strSQLPath & "\" & Path.GetFileNameWithoutExtension(ViewItem.Text) & ".rpt"
                If File.Exists(rptFile) Then
                    File.Delete(rptFile)
                End If
                Dim strScript As String = File.ReadAllText(strSQLPath & "\" & ViewItem.Text)
                Using cmd As New SqlClient.SqlCommand
                    AddHandler cmd.StatementCompleted, AddressOf OnStatementCompleted
                    cmd.Connection = conn
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = strScript
                    cmd.ExecuteNonQuery()

                End Using
            Next
        End Using
        MessageBox.Show("done")
        ChangeAndPersistSettings()
    End Sub
    Private Sub RunScriptsAsync()

        Dim strConnectionString As String = String.Empty
        Me.btnCancel.Enabled = True
        For Each ctrl In Me.Controls
            If ctrl.GetType = GetType(Button) Then
                If CType(ctrl, Button).Name <> "btnCancel" Then
                    CType(ctrl, Button).Enabled = False
                End If
            End If
        Next
        strServer = txtServer.Text

        Dim dic As New List(Of Threading.Thread)
        bAbort = False
        For Each ViewItem As ListViewItem In clbScripts.Items
            ViewItem.BackColor = Nothing
        Next

        For Each ViewItem As ListViewItem In clbScripts.CheckedItems
            'listView1.GetEmbeddedControl(1, row)
            If bAbort Then Exit For
            If ViewItem.SubItems(2).Text.Length > 0 Then
                strConnectionString = My.Settings.ConnectionString.Replace("@Server", ViewItem.SubItems(2).Text.Trim)
            Else
                strConnectionString = My.Settings.ConnectionString.Replace("@Server", strServer)
            End If

            Dim pb As New PictureBox
            pb.SizeMode = PictureBoxSizeMode.Zoom
            pb.Image = My.Resources.waiting
            clbScripts.AddEmbeddedControl(pb, 1, ViewItem.Index, DockStyle.Fill)
            clbScripts.Refresh()
            If chbSaveRpt.Checked Then
                rptFile = strSQLPath & "\" & Path.GetFileNameWithoutExtension(ViewItem.Text) & ".rpt"
                If File.Exists(rptFile) Then
                    File.Delete(rptFile)
                End If
            End If
            Dim cRunScript As New clsRunScript
            cRunScript.scriptFile = strSQLPath & "\" & ViewItem.Text
            cRunScript.reportFile = rptFile
            cRunScript.connectionString = strConnectionString
            cRunScript.listRow = ViewItem.Index
            cRunScript.saveReport = chbSaveRpt.Checked
            currentThread = New Threading.Thread(AddressOf cRunScript.runScript)
            currentThread.IsBackground = True
            currentThread.Start()
            Do While currentThread.IsAlive
                Threading.Thread.Sleep(10)
                Application.DoEvents()
                pb.Refresh()
            Loop
            Dim c As Control
            c = clbScripts.GetEmbeddedControl(1, cRunScript.listRow)
            clbScripts.RemoveEmbeddedControl(c)
            If cRunScript.returnValue = True And cRunScript.errorLevel = 0 Then
                ViewItem.BackColor = Color.LightBlue
                ViewItem.SubItems(3).Text = "Completed"
            Else
                ViewItem.BackColor = Color.LightYellow
                ViewItem.SubItems(3).Text = "Failed"
            End If

            currentThread = Nothing
        Next

        btnCancel.Enabled = False
        For Each ctrl In Me.Controls
            If ctrl.GetType = GetType(Button) Then
                If CType(ctrl, Button).Name <> "btnCancel" Then
                    CType(ctrl, Button).Enabled = True
                End If
            End If
        Next

        MessageBox.Show("done")
        ChangeAndPersistSettings()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If txtServer.Text.Length = 0 Then
            MessageBox.Show("You must enter a Server Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            RunScriptsAsync()
        End If
    End Sub
    Sub ChangeAndPersistSettings()
        My.Settings.LastPath = strSQLPath
        My.Settings.LastServer = strServer
        My.Settings.Save()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        strServer = My.Settings.LastServer
        txtServer.Text = strServer
        strSQLPath = My.Settings.LastPath
        If strSQLPath.Length > 0 Then
            txtFolder.Text = strSQLPath
            getFiles(strSQLPath)
        End If

    End Sub



    Private Sub getFiles(strPath As String)
        Dim firstLine As String = String.Empty
        clbScripts.View = View.Details
        clbScripts.GridLines = False
        Dim otherColumns As String() = {"", "", ""}
        Dim files() As String
        files = Directory.GetFiles(strPath, "*.sql", SearchOption.TopDirectoryOnly)
        For Each fileName As String In files
            Using reader As New StreamReader(fileName)
                If Not reader.EndOfStream Then firstLine = reader.ReadLine
            End Using
            If firstLine.Contains("--") Then
                otherColumns(1) = firstLine.Substring(firstLine.IndexOf("--") + 2)
            Else
                otherColumns(1) = ""
            End If
            Dim itm As ListViewItem = clbScripts.Items.Add(Path.GetFileName(fileName))
            itm.Checked = True
            itm.SubItems.AddRange(otherColumns)


        Next
    End Sub

    Private Sub clbScripts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbScripts.SelectedIndexChanged
        If clbScripts.SelectedItems.Count = 0 Then Exit Sub
        Dim viewItem As ListViewItem
        viewItem = clbScripts.SelectedItems(0)
        formatScintilla()
        Scintilla1.Text = File.ReadAllText(strSQLPath & "\" & viewItem.Text)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        bAbort = True
    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        For Each viewItem As ListViewItem In clbScripts.Items
            viewItem.Checked = True
        Next
    End Sub

    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        For Each viewItem As ListViewItem In clbScripts.Items
            viewItem.Checked = False
        Next
    End Sub

    Private Sub formatScintilla()
        ' Reset the styles
        Scintilla1.StyleResetDefault()
        Scintilla1.Styles(Style.Default).Font = "Courier New"
        Scintilla1.Styles(Style.Default).Size = 10
        Scintilla1.StyleClearAll()

        ' Set the SQL Lexer
        Scintilla1.Lexer = Lexer.Sql

        ' Show line numbers
        Scintilla1.Margins(0).Width = 20

        ' Set the Styles
        Scintilla1.Styles(Style.LineNumber).ForeColor = Color.FromArgb(255, 128, 128, 128) 'Dark Gray
        Scintilla1.Styles(Style.LineNumber).BackColor = Color.FromArgb(255, 228, 228, 228)  'Light Gray
        Scintilla1.Styles(Style.Sql.Comment).ForeColor = Color.Green
        Scintilla1.Styles(Style.Sql.CommentLine).ForeColor = Color.Green
        Scintilla1.Styles(Style.Sql.CommentLineDoc).ForeColor = Color.Green
        Scintilla1.Styles(Style.Sql.Number).ForeColor = Color.Maroon
        Scintilla1.Styles(Style.Sql.Word).ForeColor = Color.Blue
        Scintilla1.Styles(Style.Sql.Word2).ForeColor = Color.Fuchsia
        Scintilla1.Styles(Style.Sql.User1).ForeColor = Color.Gray
        Scintilla1.Styles(Style.Sql.User2).ForeColor = Color.FromArgb(255, 0, 128, 192)    'Medium Blue-Green
        Scintilla1.Styles(Style.Sql.String).ForeColor = Color.Red
        Scintilla1.Styles(Style.Sql.Character).ForeColor = Color.Red
        Scintilla1.Styles(Style.Sql.Operator).ForeColor = Color.Black

        '// Set keyword lists
        '// Word = 0
        Scintilla1.SetKeywords(0, "add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ")
        '// Word2 = 1
        Scintilla1.SetKeywords(1, "ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ")
        '// User1 = 4
        Scintilla1.SetKeywords(4, "all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ")
        '// User2 = 5
        Scintilla1.SetKeywords(5, "sys objects sysobjects ")
    End Sub
End Class
