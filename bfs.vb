Class Node
    Public x As Integer
    Public y As Integer
    Public value As Integer

    Sub New(ByVal xx As Integer, ByVal yy As Integer, ByVal val As Integer)
        x = xx
        y = yy
        value = val
    End Sub

End Class


Module Module1
    Dim rows As Integer
    Dim cols As Integer

    Dim graph(100, 100) As Integer
    Dim vis(100, 100) As Boolean
    Sub Main()
        Console.WriteLine("BFS")
        Console.WriteLine()
        Console.WriteLine()
        ' There are three kinds of nodes in the grid 
        ' 1 is a pathway you can visit this node
        ' 0 is an obstacle on that way one cannot visit
        ' 9 is to denote source and destination


        Console.WriteLine("Enter the no of rows you want in the grid : ")
        rows = Console.ReadLine()

        Console.WriteLine("Enter the no of columns you want in the grid : ")
        cols = Console.ReadLine()



        For i = 0 To rows - 1
            Dim arrayList() As String
            arrayList = Console.ReadLine().Trim().Split(" ")
            For j = 0 To cols - 1
                graph(i, j) = arrayList(j)
            Next
        Next

        Dim hm As Hashtable = New Hashtable
        Dim start As Node
        start = Nothing
        Dim endd As Node
        endd = Nothing

        For i = 0 To rows - 1
            For j = 0 To cols - 1
                If graph(i, j) = 9 Then

                    start = New Node(i, j, graph(i, j))
                    Exit For
                End If
            Next
            If IsNothing(start) = False Then
                Exit For
            End If
        Next

        If IsNothing(start) Then
            Console.WriteLine(" No source defined")
        End If

        Dim queue As ArrayList = New ArrayList
        queue.Add(start)
        hm.Add(start, Nothing)

        Dim reach_destination As Boolean
        reach_destination = False



        While reach_destination = False And queue.Count > 0
            Dim temp As Node
            temp = queue.Item(0)
            vis(temp.x, temp.y) = True
            queue.RemoveAt(0)
            Dim children As ArrayList
            children = getChildren(temp)

            For i = 0 To children.Count - 1
                If hm.ContainsKey(children.Item(i)) = False Then
                    hm.Add(children.Item(i), temp)
                    Dim val As Integer
                    val = children.Item(i).value
                    If val = 1 Then
                        queue.Add(children.Item(i))
                    End If
                    If val = 9 Then
                        reach_destination = True
                        queue.Add(children.Item(i))
                        endd = children.Item(i)
                        Exit For
                    End If
                End If

            Next
        End While

        If IsNothing(endd) Then
            Console.WriteLine("no destination possible")
        End If

        Dim path As ArrayList
        path = New ArrayList
        While IsNothing(endd) = False
            path.Add(endd)
            endd = hm.Item(endd)
        End While

        printpath(path)

        Console.ReadLine()
    End Sub

    Function getChildren(ByVal parent As Node) As ArrayList

        Dim children As ArrayList
        children = New ArrayList
        Dim x As Integer
        Dim y As Integer
        x = parent.x
        y = parent.y

        If x - 1 >= 0 Then
            If vis(x - 1, y) = False Then
                Dim child As Node
                child = New Node(x - 1, y, graph(x - 1, y))
                children.Add(child)
            End If
        End If

        If y - 1 >= 0 Then
            If vis(x, y - 1) = False Then
                Dim child As Node
                child = New Node(x, y - 1, graph(x, y - 1))
                children.Add(child)
            End If
        End If

        If x + 1 < rows Then
            If vis(x + 1, y) = False Then
                Dim child As Node
                child = New Node(x + 1, y, graph(x + 1, y))
                children.Add(child)
            End If
        End If

        If y + 1 < cols Then
            If vis(x, y + 1) = False Then
                Dim child As Node
                child = New Node(x, y + 1, graph(x, y + 1))
                children.Add(child)
            End If
        End If

            Return children

    End Function

    Function printpath(ByVal path1 As ArrayList)

        Dim value As String
        value = ""
        For i = 0 To rows - 1
            For j = 0 To cols - 1
                value = graph(i, j).ToString

                ' mark path with X
                For k = 0 To path1.Count - 1
                    Dim node As Node
                    node = path1.Item(k)
                    If node.x = i And node.y = j Then
                        value = "X"
                        Exit For
                    End If
                Next
                If j = cols - 1 Then
                    Console.WriteLine(value)

                Else
                    Console.Write(value + ".....")

                End If


            Next

            If (i < rows - 1) Then
                For ll = 0 To 2
                    For lm = 0 To cols - 2
                        Console.Write(".    ")
                    Next
                    Console.WriteLine(".    ")
                Next

            End If
            Console.WriteLine()


        Next

    End Function

End Module
