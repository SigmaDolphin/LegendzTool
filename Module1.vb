Imports System.Xml
Module Module1

    Sub LegendzIndexes(ByVal exepath As String)
        Dim count As Integer
        Dim xmlFile As XmlReader
        xmlFile = XmlReader.Create(exepath & "LegendzDB.xml")
        Dim ds As New DataSet
        Dim dv As DataView
        ds.ReadXml(xmlFile)

        'Load Legendz IDs
        dv = New DataView(ds.Tables(0))
        dv.Sort = "index"

        count = 0
        Do
            Dim index As Integer = dv.Find(count)
            If index = -1 Then
                Exit Do
            Else
                Form1.ComboBox1.Items.Insert(count, dv(index)("name").ToString())
            End If
            count = count + 1
        Loop

        'Load Elements
        dv = New DataView(ds.Tables(1))
        dv.Sort = "index"

        count = 0
        Do
            Dim index As Integer = dv.Find(count)
            If index = -1 Then
                Exit Do
            Else
                Form1.ElementC.Items.Insert(count, dv(index)("name").ToString())
            End If
            count = count + 1
        Loop

        'Load Ranks
        dv = New DataView(ds.Tables(2))
        dv.Sort = "index"

        count = 0
        Do
            Dim index As Integer = dv.Find(count)
            If index = -1 Then
                Exit Do
            Else
                Form1.RankC.Items.Insert(count, dv(index)("name").ToString())
            End If
            count = count + 1
        Loop

        'Load Temperatures
        dv = New DataView(ds.Tables(3))
        dv.Sort = "index"

        count = 0
        Do
            Dim index As Integer = dv.Find(count)
            If index = -1 Then
                Exit Do
            Else
                Form1.TemperatureC.Items.Insert(count, dv(index)("name").ToString())
            End If
            count = count + 1
        Loop

        'Load Humidity
        dv = New DataView(ds.Tables(4))
        dv.Sort = "index"

        count = 0
        Do
            Dim index As Integer = dv.Find(count)
            If index = -1 Then
                Exit Do
            Else
                Form1.HumidityC.Items.Insert(count, dv(index)("name").ToString())
            End If
            count = count + 1
        Loop

        'Load Move Names
        dv = New DataView(ds.Tables(5))
        dv.Sort = "index"

        count = 0
        Do
            Dim index As Integer = dv.Find(count)
            If index = -1 Then
                Exit Do
            Else
                Form1.ShiftE1.Items.Insert(count, dv(index)("name").ToString())
                Form1.ShiftE2.Items.Insert(count, dv(index)("name").ToString())
                Form1.ShiftE3.Items.Insert(count, dv(index)("name").ToString())

                Form1.StMove1.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove2.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove3.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove4.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove5.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove6.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove7.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove8.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove9.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove10.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove11.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove12.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove13.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove14.Items.Insert(count, dv(index)("name").ToString())
                Form1.StMove15.Items.Insert(count, dv(index)("name").ToString())

                Form1.CurMove1.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove2.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove3.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove4.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove5.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove6.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove7.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove8.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove9.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove10.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove11.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove12.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove13.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove14.Items.Insert(count, dv(index)("name").ToString())
                Form1.CurMove15.Items.Insert(count, dv(index)("name").ToString())
            End If
            count = count + 1
        Loop

    End Sub




End Module
