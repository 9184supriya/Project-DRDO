Imports System.IO

Module Module1
    Sub main()
        Dim line As String
        Using reader As StreamReader = New StreamReader("", False)
            line = reader.ReadLine()

        End Using
        Console.WriteLine(line)
    End Sub
End Module


