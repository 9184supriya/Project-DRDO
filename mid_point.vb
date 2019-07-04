
Module Module1
    Dim grid(50, 50) As Integer

    Sub main()
        For i = 0 To 50 - 1
            For j = 0 To 50 - 1
                grid(i, j) = 1
            Next
        Next

        Console.WriteLine("Enter number of tests")
        Dim x As Integer
        x = Console.ReadLine()
        For i = 0 To x - 1
            Dim radius As Integer
            Dim x_center As Integer
            Dim y_center As Integer
            radius = Console.ReadLine()
            x_center = Console.ReadLine()
            y_center = Console.ReadLine()
            Dim x_cor As Integer
            Dim y_cor As Integer
            x_cor = radius
            y_cor = 0

            If isValid(x_cor + x_center, y_cor + y_center) = True Then
                grid(x_cor + x_center, y_cor + y_center) = 0
            End If

            If radius > 0 Then
                If isValid(x_cor + x_center, -y_cor + y_center) = True Then
                    grid(x_cor + x_center, -y_cor + y_center) = 0
                End If
                If isValid(y_cor + x_center, x_cor + y_center) = True Then
                    grid(y_cor + x_center, x_cor + y_center) = 0
                End If
                If isValid(-y_cor + x_center, x_cor + y_center) = True Then
                    grid(-y_cor + x_center, x_cor + y_center) = 0
                End If
                If isValid(-x_cor + x_center, y_cor + y_center) = True Then
                    grid(-x_cor + x_center, y_cor + y_center) = 0
                End If
                If isValid(x_center, y_center - radius) = True Then
                    grid(x_center, -radius + y_center) = 0
                End If

            End If

            Dim P As Integer
            P = 1 - radius
            While x_cor > y_cor
                y_cor = y_cor + 1
                If P <= 0 Then
                    P = P + 2 * y_cor + 1
                Else
                    x_cor = x_cor - 1
                    P = P + 2 * y_cor - 2 * x_cor + 1

                End If
                If x_cor < y_cor Then
                    Exit While
                End If
                If isValid(x_cor + x_center, y_cor + y_center) = True Then
                    grid(x_cor + x_center, y_cor + y_center) = 0
                End If

                If isValid(-x_cor + x_center, y_cor + y_center) = True Then
                    grid(-x_cor + x_center, y_cor + y_center) = 0
                End If
                If isValid(x_cor + x_center, -y_cor + y_center) = True Then
                    grid(x_cor + x_center, -y_cor + y_center) = 0
                End If
                If isValid(-x_cor + x_center, -y_cor + y_center) = True Then
                    grid(-x_cor + x_center, -y_cor + y_center) = 0
                End If


                If x_cor <> y_cor Then
                    If isValid(y_cor + x_center, x_cor + y_center) = True Then
                        grid(y_cor + x_center, x_cor + y_center) = 0
                    End If
                    If isValid(-y_cor + x_center, x_cor + y_center) = True Then
                        grid(-y_cor + x_center, x_cor + y_center) = 0
                    End If
                    If isValid(y_cor + x_center, -x_cor + y_center) = True Then
                        grid(y_cor + x_center, -x_cor + y_center) = 0
                    End If
                    If isValid(-y_cor + x_center, -x_cor + y_center) = True Then
                        grid(-y_cor + x_center, -x_cor + y_center) = 0
                    End If





                End If

            End While
            For ii = 0 To 50 - 1
                For jj = 0 To 50 - 1
                    Console.Write(grid(ii, jj))
                    Console.Write(" ")
                Next
                Console.WriteLine()
            Next

        Next
        Console.ReadLine()
    End Sub
    Function isValid(ByVal x, ByVal y) As Boolean
        If x >= 0 And x < 50 And y >= 0 And y < 50 Then
            Return True
        End If
        Return False
    End Function
End Module


