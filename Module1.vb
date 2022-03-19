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
            End If
            count = count + 1
        Loop
    End Sub
End Module
