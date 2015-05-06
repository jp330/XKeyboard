Imports XKeyboard.J2i.Net.XInputWrapper
Public Class Form1


    Dim state As XInputState, prevState As XInputState
    Const max As Integer = 32767
    Const trigmax As Integer = 255
    Const threshold As Integer = 0.3 * max
    Const trigthreshold As Integer = 0.5 * trigmax

    'Dim lower As String(,) = {{"a", "b", "c", "d"}, {"e", "f", "g", "h"}, {"i", "j", "k", "l"}, {"m", "n", "o", "p"}, {"q", "r", "s", "t"}, {"u", "v", "w", "x"}, {"y", "z", ".", ","}, {"-", "@", ":", "/"}}
    'Dim upper As String(,) = {{"A", "B", "C", "D"}, {"E", "F", "G", "H"}, {"I", "J", "K", "L"}, {"M", "N", "O", "P"}, {"Q", "R", "S", "T"}, {"U", "V", "W", "X"}, {"Y", "Z", "<", ">"}, {"_", "'", "#", "?"}}
    'Dim number As String(,) = {{"1", "2", "3", "4"}, {"5", "6", "7", "8"}, {"9", "0", "!", """"}, {"£", "$", "%", "^"}, {"&", "*", "(", ")"}, {"[", "]", "{", "}"}, {"=", "+", ";", "~"}, {"\", "|", "@", "."}}

    Dim lower As String(,) = {{"a", "d", "c", "b"}, {"e", "h", "g", "f"}, {"i", "l", "k", "j"}, {"m", "p", "o", "n"}, {"q", "t", "s", "r"}, {"u", "x", "w", "v"}, {"y", ",", ".", "z"}, {"-", "@", ":", "/"}}
    Dim upper As String(,) = {{"A", "D", "C", "B"}, {"E", "H", "G", "F"}, {"I", "L", "K", "J"}, {"M", "P", "O", "N"}, {"Q", "T", "S", "R"}, {"U", "X", "W", "V"}, {"Y", "<", ">", "Z"}, {"_", "'", "#", "?"}}
    Dim number As String(,) = {{"1", "4", "3", "2"}, {"5", "8", "7", "6"}, {"9", """", "!", "0"}, {"£", "$", "%", "^"}, {"&", "*", "(", ")"}, {"[", "]", "{", "}"}, {"=", "+", ";", "~"}, {"\", "|", "@", "."}}


    Dim none As String() = {"{ENTER}", "{ESC}", " ", "{BACKSPACE}"}

    Dim isVisible As Boolean = True

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        state = New XInputState
        prevState = New XInputState
        Me.TopMost = True
        init()
        Poll.Start()
    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick


    End Sub

    Private Sub Poll_Tick(sender As Object, e As EventArgs) Handles Poll.Tick
        prevState.Copy(state)

        Dim result As Integer = XInput.XInputGetState(0, state)


        If isVisible Then
            Dim drawReq = False
            'Me.Location = Me.Location + New System.Drawing.Point((state.Gamepad.sThumbRX * 20 / 32767), -(state.Gamepad.sThumbRY * 20 / 32767))
            If Math.Abs(state.Gamepad.sThumbRX) > 0.1 * max Then
                Me.Left = Me.Left + (state.Gamepad.sThumbRX * 30 / 32767)
            End If
            If Math.Abs(state.Gamepad.sThumbRY) > 0.1 * max Then
                Me.Top = Me.Top - (state.Gamepad.sThumbRY * 30 / 32767)
            End If

            If state.Gamepad.bLeftTrigger > trigthreshold Then
                If modi <> modify.upper Then
                    modi = modify.upper
                    drawReq = True
                End If
            ElseIf state.Gamepad.bRightTrigger > trigthreshold Then
                If modi <> modify.num Then
                    modi = modify.num
                    drawReq = True
                End If
            Else
                If modi <> modify.lower Then
                    modi = modify.lower
                    drawReq = True
                End If
            End If


            If selectedSector <> Octrant(state.Gamepad) Or drawReq Then
                selectedSector = Octrant(state.Gamepad)
                Draw()
            End If
            ' Draw()
        End If


        If prevState.Gamepad.wButtons <> state.Gamepad.wButtons Then
            If prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_BACK) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_BACK)) Then
                ToggleVisible()
            End If

            If isVisible Then
                If prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_A) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_A)) Then
                    Key(state.Gamepad, Button.A)
                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_B) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_B)) Then
                    Key(state.Gamepad, Button.B)
                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_X) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_X)) Then
                    Key(state.Gamepad, Button.X)
                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_Y) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_Y)) Then
                    Key(state.Gamepad, Button.Y)
                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_LEFT_SHOULDER) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_LEFT_SHOULDER)) Then

                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_RIGHT_SHOULDER) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_RIGHT_SHOULDER)) Then

                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_DPAD_DOWN) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_DPAD_DOWN)) Then
                    SendKeys.Send("{DOWN}")
                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_DPAD_UP) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_DPAD_UP)) Then
                    SendKeys.Send("{UP}")
                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_DPAD_LEFT) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_DPAD_LEFT)) Then
                    SendKeys.Send("{LEFT}")
                ElseIf prevState.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_DPAD_RIGHT) And (Not state.Gamepad.IsButtonPressed(ButtonFlags.XINPUT_GAMEPAD_DPAD_RIGHT)) Then
                    SendKeys.Send("{RIGHT}")
                End If
                'Me.Left = Me.Left + (state.Gamepad.sThumbRX * 20 / 32767)
                'Me.Top = Me.Top - (state.Gamepad.sThumbRY * 20 / 32767)
            End If
        End If



    End Sub


    Sub ToggleVisible()
        If isVisible Then
            Me.Opacity = 0
            isVisible = False
        Else
            Me.Opacity = 1
            isVisible = True
        End If
    End Sub

    Enum Sector As Integer
        Empty = -1
        Top = 0
        TopRight = 1
        Right = 2
        BottomRight = 3
        Bottom = 4
        BottomLeft = 5
        Left = 6
        TopLeft = 7
    End Enum

    Enum Button As Integer
        A = 0
        B = 1
        Y = 2
        X = 3
    End Enum

    Function Octrant(ByRef pad As XInputGamepad) As Sector
        Dim x As Integer = pad.sThumbLX
        Dim y As Integer = pad.sThumbLY

        Dim deg As Single = Math.Atan2(x, y) + (Math.PI / 8)
        If deg < 0 Then
            deg += Math.PI * 2
        End If
        Dim h As Single = Math.Sqrt(x ^ 2 + y ^ 2)

        If h > threshold Then
            Return Math.Floor(deg / (Math.PI / 4))
        Else
            Return Sector.Empty
        End If
    End Function

    Sub Key(ByRef pad As XInputGamepad, ByVal button As Button)

        Dim sector = Octrant(pad)


        If sector = Form1.Sector.Empty Then
            SendKeys.Send(none(button))
        ElseIf pad.bLeftTrigger > trigthreshold Then
            SendKeys.Send(upper(sector, button))
        ElseIf pad.bRightTrigger > trigthreshold Then
            SendKeys.Send(SendKeyValidate(number(sector, button)))
        Else
            SendKeys.Send(lower(sector, button))
        End If

    End Sub

    Function SendKeyValidate(ByVal str As String) As String
        Select Case str
            Case "{"
                Return "{{}"
            Case "}"
                Return "{}}"
            Case "+"
                Return "{+}"
            Case "%"
                Return "{%}"
            Case "~"
                Return "{~}"
            Case "^"
                Return "{^}"
            Case "["
                Return "{[}"
            Case "]"
                Return "{]}"
            Case "("
                Return "{(}"
            Case ")"
                Return "{)}"
        End Select
        Return str
    End Function

    Enum modify
        lower = 0
        upper = 1
        num = 2
    End Enum

    Dim bigCircleColor As Brush = New SolidBrush(Color.FromArgb(26, 54, 68))
    Dim smallCircleColor As Brush = New SolidBrush(Color.FromArgb(35, 64, 78))
    Dim activatedColor As Brush = New SolidBrush(Color.FromArgb(59, 99, 120))
    Dim bigCircleRadius As Integer = 250
    Dim smallerCircles(8) As Rectangle
    Dim characterCircles(4) As Rectangle
    Dim buttonColors(4) As Brush
    Dim backbuffer As Bitmap
    Dim selectedSector As Sector
    Dim modi As modify

    Sub init()
        Dim eighthDeg = Math.PI / 8
        Dim smallCircleRadius As Integer = bigCircleRadius * Math.Sin(eighthDeg) / (1 + Math.Sin(eighthDeg))

        For i As Integer = 0 To 7
            Dim x As Integer = bigCircleRadius + (Math.Sin(i * Math.PI / 4) * (bigCircleRadius - smallCircleRadius)) - smallCircleRadius
            Dim y As Integer = bigCircleRadius - (Math.Cos(i * Math.PI / 4) * (bigCircleRadius - smallCircleRadius)) - smallCircleRadius
            smallerCircles(i) = New Rectangle(x, y, 2 * smallCircleRadius, 2 * smallCircleRadius)
        Next

        Dim w As Integer = 2 * smallCircleRadius
        characterCircles(0) = New Rectangle(0.375 * w, 0.625 * w, 0.25 * w, 0.25 * w)
        characterCircles(1) = New Rectangle(0.625 * w, 0.375 * w, 0.25 * w, 0.25 * w)
        characterCircles(2) = New Rectangle(0.375 * w, 0.125 * w, 0.25 * w, 0.25 * w)
        characterCircles(3) = New Rectangle(0.125 * w, 0.375 * w, 0.25 * w, 0.25 * w)
        buttonColors(0) = New SolidBrush(Color.FromArgb(110, 161, 3))
        buttonColors(1) = New SolidBrush(Color.FromArgb(181, 2, 1))
        buttonColors(2) = New SolidBrush(Color.FromArgb(188, 155, 2))
        buttonColors(3) = New SolidBrush(Color.FromArgb(32, 60, 76))

        backbuffer = New Bitmap(Me.Width, Me.Height)

        modi = modify.lower

        Me.TransparencyKey = Color.Black

        
    End Sub

    Sub Draw()
        Dim g As Graphics = Graphics.FromImage(backbuffer) 
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.Clear(Color.Black)

        g.FillEllipse(bigCircleColor, 0, 0, bigCircleRadius * 2, bigCircleRadius * 2)
        If selectedSector = Sector.Empty Then
            g.DrawImage(My.Resources.CenterLayout, 0, 0, backbuffer.Width, backbuffer.Height)
        End If
        Dim format As New StringFormat
        format.Alignment = StringAlignment.Center
        format.LineAlignment = StringAlignment.Center
        For i As Integer = 0 To 7
            If i = selectedSector Then
                g.FillEllipse(activatedColor, smallerCircles(i))
                For j = 0 To 3
                    Dim r As Rectangle = New Rectangle(characterCircles(j).Location, characterCircles(j).Size)
                    r.Offset(smallerCircles(i).Location)
                    g.FillEllipse(buttonColors(j), r)
                    Dim c As String
                    Select Case modi
                        Case modify.lower
                            c = lower(i, j)
                        Case modify.upper
                            c = upper(i, j)
                        Case modify.num
                            c = number(i, j)
                    End Select
                    g.DrawString(c, New Font("Segoe UI", 15, FontStyle.Bold), New SolidBrush(Color.FromArgb(254, 254, 254)), r, format)
                Next
            Else
                g.FillEllipse(smallCircleColor, smallerCircles(i))
                For j = 0 To 3
                    Dim r As Rectangle = New Rectangle(characterCircles(j).Location, characterCircles(j).Size)
                    r.Offset(smallerCircles(i).Location)
                    Dim c As String
                    Select Case modi
                        Case modify.lower
                            c = lower(i, j)
                        Case modify.upper
                            c = upper(i, j)
                        Case modify.num
                            c = number(i, j)
                    End Select
                    g.DrawString(c, New Font("Segoe UI", 15, FontStyle.Bold), New SolidBrush(Color.FromArgb(254, 254, 254)), r, format)
                Next
            End If
        Next

        g.Flush()
        g.Dispose()

        Dim gr As Graphics = Me.CreateGraphics
        gr.DrawImage(backbuffer, 0, 0)
        gr.Dispose()


    End Sub



    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Info.ShowDialog()
    End Sub
End Class
