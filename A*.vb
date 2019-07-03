/*  
* A*
* in vb.net
* 
* Created   on: June 25, 2019
* Author: goforsupriya@gmail.com
*/
'need to add more lines of code to integrate into the application

Class Node
    Public x As Integer
    Public y As Integer
    Public value As Double

    Sub New(ByVal val As Double, ByVal xx As Integer, ByVal yy As Integer)
        x = xx
        y = yy
        value = val
    End Sub

End Class

Class Node1
    Public first As Integer
    Public sec As Integer
    Sub New(ByVal f As Integer, ByVal s As Integer)
        first = f
        sec = s
    End Sub
End Class

Class Cell
    Public parent_i As Integer
    Public parent_j As Integer
    Public f As Double
    Public g As Double
    Public h As Double
    ' f = g + h
    Sub New()
        parent_i = -1
        parent_j = -1
        f = 0.0
        g = 0.0
        h = 0.0
    End Sub
End Class

Module Module1
    Dim rows As Integer
    Dim cols As Integer

    Dim graph(100, 100) As Integer
    Dim vis(100, 100) As Boolean
    Dim cdet(100, 100) As Cell
    Dim endd As Node
    Dim start As Node
    Sub Main()
        Console.WriteLine("A*")
        Console.WriteLine()
        Console.WriteLine()
        ' There are three kinds of nodes in the grid 
        ' 1 is a pathway you can visit this node
        ' 0 is an obstacle on that way one cannot visit
        ' leftmost bottommost cell is the source
        ' leftmost topmost cell is the destination


        Console.WriteLine("Enter the no of rows you want in the grid : ")
        rows = Console.ReadLine()
        Console.WriteLine("Enter the no of columns you want in the grid : ")
        cols = Console.ReadLine()

        Dim i As Integer
        Dim j As Integer

        For i = 0 To rows - 1
            Dim arrayList() As String
            arrayList = Console.ReadLine().Trim().Split(" ")
            For j = 0 To cols - 1
                graph(i, j) = arrayList(j)
            Next
        Next

        Dim hm As Hashtable = New Hashtable

        start = Nothing

        endd = Nothing

        start = New Node(graph(rows - 1, 0), rows - 1, 0)
        endd = New Node(graph(0, 0), 0, 0)

        If isValid(start.x, start.y) = False Then
            Console.WriteLine(" Source is invalid ")
            Return
        End If

        If isValid(endd.x, endd.y) = False Then
            Console.WriteLine(" Destination is invalid ")
            Return
        End If

        If isUnBlocked(start.x, start.y) = False Or isUnBlocked(endd.x, endd.y) = False Then
            Console.WriteLine(" Source or the destination is blocked ")
            Return
        End If

        If isDestination(start.x, start.y) = True Then
            Console.WriteLine("We are already at the destination")
            Return
        End If

        Dim closedList(rows, cols) As Boolean



        For i = 0 To rows - 1
            For j = 0 To cols - 1
                cdet(i, j) = New Cell
                cdet(i, j).f = 1000
                cdet(i, j).g = 1000
                cdet(i, j).h = 1000
                cdet(i, j).parent_i = -1
                cdet(i, j).parent_j = -1

            Next
        Next


        i = start.x
        j = start.y
        cdet(i, j).f = 0.0
        cdet(i, j).g = 0.0
        cdet(i, j).h = 0.0
        cdet(i, j).parent_i = i
        cdet(i, j).parent_j = j

        Dim open_list As ArrayList
        open_list = New ArrayList

        Dim node As Node
        node = New Node(0.0, i, j)
        open_list.Add(node)

        Dim destination_reached As Boolean
        destination_reached = False

        While open_list.Count > 0
            Dim sup As Node
            sup = open_list.Item(0)
            open_list.RemoveAt(0)
            i = sup.x
            j = sup.y
            closedList(i, j) = True

            '8 connected traversal is considered by me
            'Cell--> Popped Cell (i, j) 
            'N --> North(i - 1, j) 
            'S --> South(i + 1, j) 
            'E --> East(i, j + 1) 
            'W --> West(i, j - 1) 
            'N.E--> North - East(i - 1, j + 1) 
            'N.W--> North - West(i - 1, j - 1) 
            'S.E--> South - East(i + 1, j + 1) 
            'S.W--> South - West(i + 1, j - 1) 

            Dim fnew As Double
            Dim gnew As Double
            Dim hnew As Double
            'fnew = gnew + hnew

            'Let's travel North first
            If isValid(i - 1, j) = True Then
                If isDestination(i - 1, j) = True Then
                    cdet(i - 1, j).parent_i = i
                    cdet(i - 1, j).parent_j = j
                    Console.WriteLine(" Destination is reached ")
                    tracePath(cdet(i - 1, j).parent_i, cdet(i - 1, j).parent_j)
                    destination_reached = True
                    Console.ReadLine()
                    Return
                ElseIf isUnBlocked(i - 1, j) = True And closedList(i - 1, j) = False Then
                    gnew = cdet(i, j).g + 1.0
                    hnew = calculateHValue(i - 1, j)
                    fnew = gnew + hnew



                    If cdet(i - 1, j).f = 1000 Or cdet(i - 1, j).f > fnew Then
                        open_list.Add(New Node(fnew, i - 1, j))
                        cdet(i - 1, j).f = fnew
                        cdet(i - 1, j).g = gnew
                        cdet(i - 1, j).h = hnew
                        cdet(i - 1, j).parent_i = i
                        cdet(i - 1, j).parent_j = j

                    End If
                End If
            End If

            'Let's travel South succesor

            If isValid(i + 1, j) = True Then
                If isDestination(i + 1, j) = True Then
                    cdet(i + 1, j).parent_i = i
                    cdet(i + 1, j).parent_j = j
                    Console.WriteLine(" Destination is reached ")
                    tracePath(cdet(i + 1, j).parent_i, cdet(i + 1, j).parent_j)
                    destination_reached = True
                    Console.ReadLine()
                    Return
                ElseIf isUnBlocked(i + 1, j) = True And closedList(i + 1, j) = False Then
                    gnew = cdet(i, j).g + 1.0
                    hnew = calculateHValue(i + 1, j)
                    fnew = gnew + hnew



                    If cdet(i + 1, j).f = 1000 Or cdet(i + 1, j).f > fnew Then
                        open_list.Add(New Node(fnew, i + 1, j))
                        cdet(i + 1, j).f = fnew
                        cdet(i + 1, j).g = gnew
                        cdet(i + 1, j).h = hnew
                        cdet(i + 1, j).parent_i = i
                        cdet(i + 1, j).parent_j = j

                    End If
                End If
            End If

            'Let's travel East-> succesor

            If isValid(i, j + 1) = True Then
                If isDestination(i, j + 1) = True Then
                    cdet(i, j + 1).parent_i = i
                    cdet(i, j + 1).parent_j = j
                    Console.WriteLine(" Destination is reached ")
                    tracePath(cdet(i, j + 1).parent_i, cdet(i, j + 1).parent_j)
                    destination_reached = True
                    Console.ReadLine()
                    Return
                ElseIf isUnBlocked(i, j + 1) = True And closedList(i, j + 1) = False Then
                    gnew = cdet(i, j).g + 1.0
                    hnew = calculateHValue(i, j + 1)
                    fnew = gnew + hnew



                    If cdet(i, j + 1).f = 1000 Or cdet(i, j + 1).f > fnew Then
                        open_list.Add(New Node(fnew, i, j + 1))
                        cdet(i, j + 1).f = fnew
                        cdet(i, j + 1).g = gnew
                        cdet(i, j + 1).h = hnew
                        cdet(i, j + 1).parent_i = i
                        cdet(i, j + 1).parent_j = j

                    End If
                End If
            End If

            'Let's travel West<- succesor

            If isValid(i, j - 1) = True Then
                If isDestination(i, j - 1) = True Then
                    cdet(i, j - 1).parent_i = i
                    cdet(i, j - 1).parent_j = j
                    Console.WriteLine(" Destination is reached ")
                    tracePath(cdet(i, j - 1).parent_i, cdet(i, j - 1).parent_j)
                    destination_reached = True
                    Console.ReadLine()
                    Return
                ElseIf isUnBlocked(i, j - 1) = True And closedList(i, j - 1) = False Then
                    gnew = cdet(i, j).g + 1.0
                    hnew = calculateHValue(i, j - 1)
                    fnew = gnew + hnew



                    If cdet(i, j - 1).f = 1000 Or cdet(i, j - 1).f > fnew Then
                        open_list.Add(New Node(fnew, i, j - 1))
                        cdet(i, j - 1).f = fnew
                        cdet(i, j - 1).g = gnew
                        cdet(i, j - 1).h = hnew
                        cdet(i, j - 1).parent_i = i
                        cdet(i, j - 1).parent_j = j

                    End If
                End If
            End If

            'Let's travel SouthEast->-> succesor

            If isValid(i + 1, j + 1) = True Then
                If isDestination(i + 1, j + 1) = True Then
                    cdet(i + 1, j + 1).parent_i = i
                    cdet(i + 1, j + 1).parent_j = j
                    Console.WriteLine(" Destination is reached ")
                    tracePath(cdet(i + 1, j + 1).parent_i, cdet(i + 1, j + 1).parent_j)
                    destination_reached = True
                    Console.ReadLine()
                    Return
                ElseIf isUnBlocked(i + 1, j + 1) = True And closedList(i + 1, j + 1) = False Then
                    gnew = cdet(i, j).g + 1.414
                    hnew = calculateHValue(i + 1, j + 1)
                    fnew = gnew + hnew



                    If cdet(i + 1, j + 1).f = 1000 Or cdet(i + 1, j + 1).f > fnew Then
                        open_list.Add(New Node(fnew, i + 1, j + 1))
                        cdet(i + 1, j + 1).f = fnew
                        cdet(i + 1, j + 1).g = gnew
                        cdet(i + 1, j + 1).h = hnew
                        cdet(i + 1, j + 1).parent_i = i
                        cdet(i + 1, j + 1).parent_j = j

                    End If
                End If
            End If

            'Let's travel NorthEast<- -> succesor

            If isValid(i - 1, j + 1) = True Then
                If isDestination(i - 1, j + 1) = True Then
                    cdet(i - 1, j + 1).parent_i = i
                    cdet(i - 1, j + 1).parent_j = j
                    Console.WriteLine(" Destination is reached ")
                    tracePath(cdet(i - 1, j + 1).parent_i, cdet(i - 1, j + 1).parent_j)
                    destination_reached = True
                    Console.ReadLine()
                    Return
                ElseIf isUnBlocked(i - 1, j + 1) = True And closedList(i - 1, j + 1) = False Then
                    gnew = cdet(i, j).g + 1.414
                    hnew = calculateHValue(i - 1, j + 1)
                    fnew = gnew + hnew



                    If cdet(i - 1, j + 1).f = 1000 Or cdet(i - 1, j + 1).f > fnew Then
                        open_list.Add(New Node(fnew, i - 1, j + 1))
                        cdet(i - 1, j + 1).f = fnew
                        cdet(i - 1, j + 1).g = gnew
                        cdet(i - 1, j + 1).h = hnew
                        cdet(i - 1, j + 1).parent_i = i
                        cdet(i - 1, j + 1).parent_j = j

                    End If
                End If
            End If

            'Let's travel NorthWest<-<- succesor

            If isValid(i - 1, j - 1) = True Then
                If isDestination(i - 1, j - 1) = True Then
                    cdet(i - 1, j - 1).parent_i = i
                    cdet(i - 1, j - 1).parent_j = j
                    Console.WriteLine(" Destination is reached ")
                    tracePath(cdet(i - 1, j - 1).parent_i, cdet(i - 1, j - 1).parent_j)
                    destination_reached = True
                    Console.ReadLine()
                    Return
                ElseIf isUnBlocked(i - 1, j - 1) = True And closedList(i - 1, j - 1) = False Then
                    gnew = cdet(i, j).g + 1.414
                    hnew = calculateHValue(i - 1, j - 1)
                    fnew = gnew + hnew



                    If cdet(i - 1, j - 1).f = 1000 Or cdet(i - 1, j - 1).f > fnew Then
                        open_list.Add(New Node(fnew, i - 1, j - 1))
                        cdet(i - 1, j - 1).f = fnew
                        cdet(i - 1, j - 1).g = gnew
                        cdet(i - 1, j - 1).h = hnew
                        cdet(i - 1, j - 1).parent_i = i
                        cdet(i - 1, j - 1).parent_j = j

                    End If
                End If
            End If

            'Let's travel SouthWets -> <- succesor

            If isValid(i + 1, j - 1) = True Then
                If isDestination(i + 1, j - 1) = True Then
                    cdet(i + 1, j - 1).parent_i = i
                    cdet(i + 1, j - 1).parent_j = j
                    Console.WriteLine(" Destination is reached ")
                    tracePath(cdet(i + 1, j - 1).parent_i, cdet(i + 1, j - 1).parent_j)
                    destination_reached = True
                    Console.ReadLine()
                    Return
                ElseIf isUnBlocked(i + 1, j - 1) = True And closedList(i + 1, j - 1) = False Then
                    gnew = cdet(i, j).g + 1.414
                    hnew = calculateHValue(i + 1, j - 1)
                    fnew = gnew + hnew



                    If cdet(i + 1, j - 1).f = 1000 Or cdet(i + 1, j - 1).f > fnew Then
                        open_list.Add(New Node(fnew, i + 1, j - 1))
                        cdet(i + 1, j - 1).f = fnew
                        cdet(i + 1, j - 1).g = gnew
                        cdet(i + 1, j - 1).h = hnew
                        cdet(i + 1, j - 1).parent_i = i
                        cdet(i + 1, j - 1).parent_j = j

                    End If
                End If
            End If

        End While
        If destination_reached = False Then
            Console.WriteLine("Failed to find the Destination Cell")
        End If
        Console.ReadLine()



    End Sub

    Function tracePath(ByVal xxx As Integer, ByVal yyy As Integer)
        Console.WriteLine("the Path is ")
        Dim row As Integer
        Dim col As Integer
        row = endd.x
        col = endd.y
        Dim path As Stack
        path = New Stack

        While cdet(row, col).parent_i = xxx And cdet(row, col).parent_j = yyy

            If row = rows - 1 And col = 0 Then
                Exit While
            End If
            Dim pp As Node1
        pp = New Node1(row, col)
            path.Push(pp)
            Dim temp_row As Integer
            Dim temp_col As Integer
            temp_row = cdet(row, col).parent_i
            temp_col = cdet(row, col).parent_j
            row = temp_row
            col = temp_col
            xxx = cdet(row, col).parent_i
            yyy = cdet(row, col).parent_j



        End While
        Console.WriteLine(path.Count)
        path.Push(New Node1(row, col))
        While (path.Count > 0)
            Dim ppp As Node1
            ppp = path.Peek()
            path.Pop()
            Console.WriteLine("-> ({0}{1})", ppp.first, ppp.sec)

        End While


    End Function

    Function calculateHValue(ByVal row As Integer, ByVal col As Integer) As Double
        'return using dist formula :] supriya loves this
        Dim ans As Double
        ans = Math.Sqrt((row - endd.x) * (row - endd.x) + (col - endd.y) * (col - endd.y))
        Return ans
    End Function

    Function isValid(ByVal row As Integer, ByVal col As Integer) As Boolean
        ' Returns true if row and col is in range
        Dim ans As Boolean
        ans = False
        If row >= 0 And row < rows And col >= 0 And col < cols Then
            ans = True
        End If
        Return ans
    End Function



    Function isUnBlocked(ByVal row As Integer, ByVal col As Integer) As Boolean
        ' Returns true if not blocked and false if blocked
        If graph(row, col) = 1 Then
            Return True
        Else
            Return False
        End If
    End Function



    Function isDestination(ByVal row As Integer, ByVal col As Integer) As Boolean
        If row = endd.x And col = endd.y Then
            Return True
        Else
            Return False
        End If
    End Function

'Finally hardwork successful ;]
End Module
