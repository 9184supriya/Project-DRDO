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


Module Module1
    Dim population_size As Integer
    Dim grid_size As Integer
    Dim grid(100, 100) As Integer
    Dim dest_i As Integer
    Dim dest_j As Integer
    Dim cur_pos_row(100) As Integer
    Dim cur_pos_col(100) As Integer
    Dim cur_velocity(100) As Double
    Dim pbest_velocity(100) As Double
    Dim pbest_position_row(100) As Integer
    Dim pbest_position_col(100) As Integer
    Dim max_iteration As Integer
    Dim current_fitness(100) As Double
    Dim pbest_fitness(100) As Double
    Dim gbest_fitness As Double
    Dim gbest_part_i As Integer
    Dim gbest_part_j As Integer
    Dim c1 As Double
    Dim c2 As Double
    Dim w As Double
    Dim r1 As Double
    Dim r2 As Double

    Sub main()
        Console.WriteLine("Enter the number of rows of the input grid")
        grid_size = Console.ReadLine()

        generate_grid()
        print_grid()

        Console.WriteLine("Choose a destination row and col for the grid having no obstacles")
        dest_i = Console.ReadLine()
        dest_j = Console.ReadLine()
        population_size = Math.Sqrt(grid_size)

        initialize_position_vector()

        For i = 0 To population_size
            Console.WriteLine(cur_pos_row(i))
            Console.WriteLine(cur_pos_col(i))
        Next

        initialize_velocity_vector()

        For i = 0 To population_size - 1
            pbest_velocity(i) = cur_velocity(i)
        Next

        For i = 0 To population_size - 1
            pbest_position_col(i) = cur_pos_col(i)
            pbest_position_row(i) = cur_pos_row(i)
        Next
        calculate_current_fitness()

        For i = 0 To population_size - 1
            pbest_fitness(i) = current_fitness(i)
        Next
        calculate_global_best()
        max_iteration = 500

        c1 = 0.2
        c2 = 0.2
        r1 = 0.3
        r2 = 0.5
        w = 0.6

        While max_iteration > 0

            update_current_velocity()
            update_current_position()
            calculate_current_fitness()
            For i = 0 To population_size
                If pbest_fitness(i) > current_fitness(i) Then
                    pbest_fitness(i) = current_fitness(i)
                    pbest_position_col(i) = cur_pos_col(i)
                    pbest_position_row(i) = cur_pos_row(i)
                End If
            Next
            calculate_global_best()
            max_iteration = max_iteration - 1

            For i = 0 To population_size
                Console.WriteLine(cur_pos_row(i))
                Console.WriteLine(cur_pos_col(i))
            Next
        End While

        For i = 0 To population_size - 1
            Console.WriteLine(pbest_position_col(i))
            Console.WriteLine(pbest_position_row(i))
        Next

        Console.ReadLine()
    End Sub

    Function generate_grid()
        Dim val As Integer
        val = (grid_size * grid_size) / 5
        For i = 0 To grid_size - 1
            For j = 0 To grid_size - 1
                grid(i, j) = 1
            Next
        Next

        For i = 0 To val - 1
            Dim ii As Integer
            Dim jj As Integer
            Dim upperbound As Integer
            Dim lowerbound As Integer
            upperbound = grid_size
            lowerbound = 0
            ii = Int((upperbound - lowerbound - 1) * Rnd() + lowerbound)
            jj = Int((upperbound - lowerbound - 1) * Rnd() + lowerbound)
            grid(ii, jj) = 0
        Next

    End Function

    Function print_grid()
        For i = 0 To grid_size - 1
            For j = 0 To grid_size - 1
                Console.Write(grid(i, j))
                Console.Write(" ")
            Next
            Console.WriteLine()
        Next
    End Function

    Function initialize_position_vector()
        For i = 0 To population_size - 1
            Dim ii As Integer
            Dim jj As Integer
            Do

                Dim upperbound As Integer
                Dim lowerbound As Integer
                upperbound = grid_size
                lowerbound = 1
                ii = Int((upperbound - lowerbound) * Rnd() + lowerbound)
                jj = Int((upperbound - lowerbound) * Rnd() + lowerbound)
            Loop Until grid(ii, jj) = 1
            cur_pos_col(i) = jj
            cur_pos_row(i) = ii
        Next

    End Function

    Function initialize_velocity_vector()
        For i = 0 To population_size - 1
            Dim upper As Integer
            Dim lower As Integer
            upper = grid_size
            lower = 0
            cur_velocity(i) = Rnd() * (upper - lower) + lower

        Next
    End Function

    Function calculate_current_fitness()
        For i = 0 To population_size - 1
            Dim value As Double
            value = Math.Sqrt((dest_i - cur_pos_row(i)) * (dest_i - cur_pos_row(i)) + (dest_j - cur_pos_col(i)) * (dest_j - cur_pos_col(i)))
            current_fitness(i) = value
        Next
    End Function

    Function calculate_global_best()
        Dim min As Double

        For i = 0 To population_size - 1
            If pbest_fitness(i) < gbest_fitness Then
                gbest_fitness = pbest_fitness(i)
                gbest_part_i = pbest_position_row(i)
                gbest_part_j = pbest_position_col(i)
            End If

        Next

    End Function

    Function update_current_velocity()
        For i = 0 To population_size - 1
            cur_velocity(i) = w * pbest_velocity(i) + c1 * r1 * (pbest_fitness(i) - current_fitness(i)) + (r2 * c2 * (gbest_fitness - current_fitness(i)))

        Next

    End Function

    Function update_current_position()
        Dim xx As Integer
        Dim yy As Integer
        For i = 0 To population_size
            xx = cur_pos_row(i)
            yy = cur_pos_col(i)
            cur_pos_row(i) = cur_pos_row(i) + cur_velocity(i)
            cur_pos_col(i) = cur_pos_col(i) + cur_velocity(i)
            If (cur_pos_row(i) >= 0 And cur_pos_col(i) < population_size - 1 And grid(cur_pos_col(i), cur_pos_row(i)) = 1) = False Then
                cur_pos_row(i) = xx
                cur_pos_col(i) = yy
            End If
        Next
    End Function



End Module
