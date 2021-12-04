Public Class Form1
    Dim Legendz(1023) As Byte
    Dim singleByte(0) As Byte
    Dim filePath As String
    Dim offset As Integer
    Dim spriteIndex As Integer
    Dim oDialog As New OpenFileDialog
    Dim sDialog As New SaveFileDialog
    Dim sprite As New Bitmap(24, 32)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LegendzDB()
        spriteIndex = 0
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox2.Items.Add(sp)
        Next
    End Sub

    'Load File
    Private Sub LoadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadButton.Click
        oDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*"
        oDialog.ShowDialog()
        If oDialog.FileName = "" Then
            Exit Sub
        End If
        filePath = oDialog.FileName
        oDialog.FileName = ""
        Legendz = System.IO.File.ReadAllBytes(filePath)
        ComboBox1.SelectedIndex = Legendz(770)
        Label2.Text = 0
        traceSprite(0)
    End Sub

    'Save File
    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        sDialog.DefaultExt() = ".bin"
        sDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*"
        sDialog.ShowDialog()
        If sDialog.FileName = "" Then
            Exit Sub
        End If
        filePath = sDialog.FileName
        sDialog.FileName = ""
        System.IO.File.WriteAllBytes(filePath, Legendz)
    End Sub

    'Write to Chip
    Private Sub WriteButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WriteButton.Click
        offset = 0
        SerialPort1.PortName = ComboBox2.SelectedItem
        SerialPort1.Open()
        SerialPort1.Write("W")
        Do
            System.Threading.Thread.Sleep(20)
            SerialPort1.Write(Legendz, offset, 1)
            System.Threading.Thread.Sleep(20)
            offset = offset + 1
            Application.DoEvents()
        Loop While offset < 1024
        SerialPort1.Close()
        MsgBox("Write Complete")
    End Sub

    'Read from Chip
    Private Sub ReadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadButton.Click
        offset = 0
        SerialPort1.PortName = ComboBox2.SelectedItem
        SerialPort1.Open()
        SerialPort1.Write("R")
        Do
            Legendz(offset) = SerialPort1.ReadByte()
            offset = offset + 1
            Application.DoEvents()
        Loop While offset < 1024
        SerialPort1.Close()
        ComboBox1.SelectedIndex = Legendz(770)
        Label2.Text = 0
        traceSprite(0)
    End Sub

    'Change ID in Chip
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        singleByte(0) = ComboBox1.SelectedIndex
        SerialPort1.PortName = ComboBox2.SelectedItem
        SerialPort1.Open()
        SerialPort1.Write("1")
        SerialPort1.Write(singleByte, 0, 1)
        SerialPort1.Close()
    End Sub


    Sub traceSprite(ByVal index As Integer)
        Dim x As Integer
        Dim y As Integer
        Dim spriteLoop As Integer
        Dim strBuffer As String
        Dim strBuffer2 As String
        Dim strBuffer3 As String
        Dim binaryString As String
        sprite = New Bitmap(24, 32)
        PictureBox1.Image = Nothing

        spriteLoop = 96 * index
        y = 0

        Do While y <= 31

            strBuffer = "00000000" & Convert.ToString(Legendz(spriteLoop), 2)
            strBuffer = strBuffer.Substring(strBuffer.Length - 8)
            strBuffer2 = "00000000" & Convert.ToString(Legendz(spriteLoop + 1), 2)
            strBuffer2 = strBuffer2.Substring(strBuffer2.Length - 8)
            strBuffer3 = "00000000" & Convert.ToString(Legendz(spriteLoop + 2), 2)
            strBuffer3 = strBuffer3.Substring(strBuffer3.Length - 8)
            binaryString = strBuffer & strBuffer2 & strBuffer3

            x = 0
            Dim a
            For Each a In binaryString
                If a = "1" Then
                    sprite.SetPixel(x, y, Color.Black)
                Else
                    sprite.SetPixel(x, y, Color.White)
                End If
                x = x + 1
            Next
            spriteLoop = spriteLoop + 3
            y = y + 1
        Loop
        PictureBox1.Image = sprite
        PictureBox1.Refresh()
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        e.Graphics.ScaleTransform(10, 10)
        e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        e.Graphics.DrawImage(sprite, 0, 0)
    End Sub

    'Next sprite
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Label2.Text = Label2.Text + 1
        If Label2.Text = 8 Then
            Label2.Text = 0
        End If
        spriteIndex = Label2.Text
        traceSprite(spriteIndex)
    End Sub

    'Save selected sprite
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        sDialog.DefaultExt() = ".png"
        sDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*"
        sDialog.ShowDialog()
        If sDialog.FileName = "" Then
            Exit Sub
        End If
        filePath = sDialog.FileName
        sDialog.FileName = ""
        PictureBox1.Image.Save(filePath, Imaging.ImageFormat.Png)
    End Sub

    'Save Sprite sheet
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sDialog.DefaultExt() = ".png"
        sDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*"
        sDialog.ShowDialog()
        If sDialog.FileName = "" Then
            Exit Sub
        End If
        filePath = sDialog.FileName
        sDialog.FileName = ""
        drawSheet.Save(filePath, Imaging.ImageFormat.Png)
    End Sub

    Function drawSheet() As Image
        Dim spriteSheet As Bitmap
        Dim x As Integer
        Dim y As Integer
        Dim spriteLoop As Integer = 0
        Dim strBuffer As String
        Dim strBuffer2 As String
        Dim strBuffer3 As String
        Dim binaryString As String

        Dim spriteCount As Integer
        Dim rowCount As Integer


        spriteSheet = New Bitmap(96, 64)

        For rowCount = 0 To 1
            For spriteCount = 0 To 3
                y = 32 * rowCount
                Do While y <= (32 * (rowCount + 1)) - 1

                    strBuffer = "00000000" & Convert.ToString(Legendz(spriteLoop), 2)
                    strBuffer = strBuffer.Substring(strBuffer.Length - 8)
                    strBuffer2 = "00000000" & Convert.ToString(Legendz(spriteLoop + 1), 2)
                    strBuffer2 = strBuffer2.Substring(strBuffer2.Length - 8)
                    strBuffer3 = "00000000" & Convert.ToString(Legendz(spriteLoop + 2), 2)
                    strBuffer3 = strBuffer3.Substring(strBuffer3.Length - 8)
                    binaryString = strBuffer & strBuffer2 & strBuffer3

                    x = 24 * spriteCount
                    Dim a
                    For Each a In binaryString
                        If a = "1" Then
                            spriteSheet.SetPixel(x, y, Color.Black)
                        Else
                            spriteSheet.SetPixel(x, y, Color.White)
                        End If
                        x += 1
                    Next
                    spriteLoop += 3
                    y += 1
                Loop
            Next
        Next
        Return spriteSheet
    End Function

    'Load sprite sheet
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        oDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*"
        oDialog.ShowDialog()
        If oDialog.FileName = "" Then
            Exit Sub
        End If
        filePath = oDialog.FileName
        oDialog.FileName = ""
        readSheet()
        Label2.Text = 0
        traceSprite(0)
    End Sub

    Sub readSheet()
        Dim spriteSheet As New Bitmap(filePath)
        Dim x As Integer
        Dim y As Integer
        Dim spriteLoop As Integer = 0

        Dim byteBuffer As Byte
        Dim spriteCount As Integer
        Dim rowCount As Integer
        Dim sectionCount As Integer

        If spriteSheet.Width <> 96 Or spriteSheet.Height <> 64 Then
            MsgBox("Wrong image size, image needs to be 96x64")
            Exit Sub
        End If

        For rowCount = 0 To 1
            For spriteCount = 0 To 3
                y = 32 * rowCount

                Do While y <= (32 * (rowCount + 1)) - 1
                    x = 24 * spriteCount
                    For sectionCount = 0 To 2
                        For b = 0 To 7
                            If spriteSheet.GetPixel(b + (8 * sectionCount) + x, y).R <> 255 Then
                                byteBuffer += 1
                            End If
                            If b <> 7 Then
                                byteBuffer <<= 1
                            End If
                        Next
                        Legendz(spriteLoop) = byteBuffer
                        spriteLoop += 1
                        byteBuffer = 0
                    Next

                    y += 1
                Loop
            Next
        Next
    End Sub
End Class
