Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading.Tasks
Imports System.Threading

Namespace J2i.Net.XInputWrapper
    <StructLayout(LayoutKind.Explicit)> _
    Public Structure XInputGamepad
        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(0)> _
        Public wButtons As Short

        <MarshalAs(UnmanagedType.I1)> _
        <FieldOffset(2)> _
        Public bLeftTrigger As Byte

        <MarshalAs(UnmanagedType.I1)> _
        <FieldOffset(3)> _
        Public bRightTrigger As Byte

        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(4)> _
        Public sThumbLX As Short

        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(6)> _
        Public sThumbLY As Short

        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(8)> _
        Public sThumbRX As Short

        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(10)> _
        Public sThumbRY As Short


        Public Function IsButtonPressed(buttonFlags As Integer) As Boolean
            Return (wButtons And buttonFlags) = buttonFlags
        End Function

        Public Function IsButtonPresent(buttonFlags As Integer) As Boolean
            Return (wButtons And buttonFlags) = buttonFlags
        End Function



        Public Sub Copy(source As XInputGamepad)
            sThumbLX = source.sThumbLX
            sThumbLY = source.sThumbLY
            sThumbRX = source.sThumbRX
            sThumbRY = source.sThumbRY
            bLeftTrigger = source.bLeftTrigger
            bRightTrigger = source.bRightTrigger
            wButtons = source.wButtons
        End Sub

        Public Overrides Function Equals(obj As Object) As Boolean
            If Not (TypeOf obj Is XInputGamepad) Then
                Return False
            End If
            Dim source As XInputGamepad = CType(obj, XInputGamepad)
            Return ((sThumbLX = source.sThumbLX) AndAlso (sThumbLY = source.sThumbLY) AndAlso (sThumbRX = source.sThumbRX) AndAlso (sThumbRY = source.sThumbRY) AndAlso (bLeftTrigger = source.bLeftTrigger) AndAlso (bRightTrigger = source.bRightTrigger) AndAlso (wButtons = source.wButtons))
        End Function
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure XInputVibration
        <MarshalAs(UnmanagedType.I2)> _
        Public LeftMotorSpeed As UShort

        <MarshalAs(UnmanagedType.I2)> _
        Public RightMotorSpeed As UShort
    End Structure

    <StructLayout(LayoutKind.Explicit)> _
    Public Structure XInputState
        <FieldOffset(0)> _
        Public PacketNumber As Integer

        <FieldOffset(4)> _
        Public Gamepad As XInputGamepad

        Public Sub Copy(source As XInputState)
            PacketNumber = source.PacketNumber
            Gamepad.Copy(source.Gamepad)
        End Sub

        Public Overrides Function Equals(obj As Object) As Boolean
            If (obj Is Nothing) OrElse (Not (TypeOf obj Is XInputState)) Then
                Return False
            End If
            Dim source As XInputState = CType(obj, XInputState)

            Return ((PacketNumber = source.PacketNumber) AndAlso (Gamepad.Equals(source.Gamepad)))
        End Function
    End Structure

    <StructLayout(LayoutKind.Explicit)> _
    Public Structure XInputKeystroke
        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(0)> _
        Public VirtualKey As Short

        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(2)> _
        Public Unicode As Char

        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(4)> _
        Public Flags As Short

        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(5)> _
        Public UserIndex As Byte

        <MarshalAs(UnmanagedType.I1)> _
        <FieldOffset(6)> _
        Public HidCode As Byte
    End Structure

    <Flags> _
    Public Enum ButtonFlags As Integer
        XINPUT_GAMEPAD_DPAD_UP = &H1
        XINPUT_GAMEPAD_DPAD_DOWN = &H2
        XINPUT_GAMEPAD_DPAD_LEFT = &H4
        XINPUT_GAMEPAD_DPAD_RIGHT = &H8
        XINPUT_GAMEPAD_START = &H10
        XINPUT_GAMEPAD_BACK = &H20
        XINPUT_GAMEPAD_LEFT_THUMB = &H40
        XINPUT_GAMEPAD_RIGHT_THUMB = &H80
        XINPUT_GAMEPAD_LEFT_SHOULDER = &H100
        XINPUT_GAMEPAD_RIGHT_SHOULDER = &H200
        XINPUT_GAMEPAD_A = &H1000
        XINPUT_GAMEPAD_B = &H2000
        XINPUT_GAMEPAD_X = &H4000
        XINPUT_GAMEPAD_Y = &H8000
    End Enum

    <Flags> _
    Public Enum ControllerSubtypes
        XINPUT_DEVSUBTYPE_UNKNOWN = &H0
        XINPUT_DEVSUBTYPE_WHEEL = &H2
        XINPUT_DEVSUBTYPE_ARCADE_STICK = &H3
        XINPUT_DEVSUBTYPE_FLIGHT_STICK = &H4
        XINPUT_DEVSUBTYPE_DANCE_PAD = &H5
        XINPUT_DEVSUBTYPE_GUITAR = &H6
        XINPUT_DEVSUBTYPE_GUITAR_ALTERNATE = &H7
        XINPUT_DEVSUBTYPE_DRUM_KIT = &H8
        XINPUT_DEVSUBTYPE_GUITAR_BASS = &HB
        XINPUT_DEVSUBTYPE_ARCADE_PAD = &H13
    End Enum

    Public Enum BatteryTypes As Byte
        '
        ' Flags for battery status level
        '
        BATTERY_TYPE_DISCONNECTED = &H0
        ' This device is not connected
        BATTERY_TYPE_WIRED = &H1
        ' Wired device, no battery
        BATTERY_TYPE_ALKALINE = &H2
        ' Alkaline battery source
        BATTERY_TYPE_NIMH = &H3
        ' Nickel Metal Hydride battery source
        BATTERY_TYPE_UNKNOWN = &HFF
        ' Cannot determine the battery type
    End Enum


    ' These are only valid for wireless, connected devices, with known battery types
    ' The amount of use time remaining depends on the type of device.
    Public Enum BatteryLevel As Byte
        BATTERY_LEVEL_EMPTY = &H0
        BATTERY_LEVEL_LOW = &H1
        BATTERY_LEVEL_MEDIUM = &H2
        BATTERY_LEVEL_FULL = &H3
    End Enum

    Public Enum BatteryDeviceType As Byte
        BATTERY_DEVTYPE_GAMEPAD = &H0
        BATTERY_DEVTYPE_HEADSET = &H1
    End Enum

    Public Class XInputConstants
        Public Const XINPUT_DEVTYPE_GAMEPAD As Integer = &H1

        '
        ' Device subtypes available in XINPUT_CAPABILITIES
        '
        Public Const XINPUT_DEVSUBTYPE_GAMEPAD As Integer = &H1

        '
        ' Flags for XINPUT_CAPABILITIES
        '
        Public Enum CapabilityFlags
            XINPUT_CAPS_VOICE_SUPPORTED = &H4
            'For Windows 8 only
            XINPUT_CAPS_FFB_SUPPORTED = &H1
            XINPUT_CAPS_WIRELESS = &H2
            XINPUT_CAPS_PMD_SUPPORTED = &H8
            XINPUT_CAPS_NO_NAVIGATION = &H10
        End Enum
        '
        ' Constants for gamepad buttons
        '

        '
        ' Gamepad thresholds
        '
        Public Const XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE As Integer = 7849
        Public Const XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE As Integer = 8689
        Public Const XINPUT_GAMEPAD_TRIGGER_THRESHOLD As Integer = 30

        '
        ' Flags to pass to XInputGetCapabilities
        '
        Public Const XINPUT_FLAG_GAMEPAD As Integer = &H1


    End Class


    <StructLayout(LayoutKind.Explicit)> _
    Public Structure XInputCapabilities
        <MarshalAs(UnmanagedType.I1)> _
        <FieldOffset(0)> _
        Private Type As Byte

        <MarshalAs(UnmanagedType.I1)> _
        <FieldOffset(1)> _
        Public SubType As Byte

        <MarshalAs(UnmanagedType.I2)> _
        <FieldOffset(2)> _
        Public Flags As Short


        <FieldOffset(4)> _
        Public Gamepad As XInputGamepad

        <FieldOffset(16)> _
        Public Vibration As XInputVibration
    End Structure

    <StructLayout(LayoutKind.Explicit)> _
    Public Structure XInputBatteryInformation
        <MarshalAs(UnmanagedType.I1)> _
        <FieldOffset(0)> _
        Public BatteryType As Byte

        <MarshalAs(UnmanagedType.I1)> _
        <FieldOffset(1)> _
        Public BatteryLevel As Byte

        Public Overrides Function ToString() As String
            Return String.Format("{0} {1}", CType(BatteryType, BatteryTypes), CType(BatteryLevel, BatteryLevel))
        End Function
    End Structure
    Public Class Point
        Public Property X() As Integer
            Get
                Return m_X
            End Get
            Set(value As Integer)
                m_X = Value
            End Set
        End Property
        Private m_X As Integer
        Public Property Y() As Integer
            Get
                Return m_Y
            End Get
            Set(value As Integer)
                m_Y = Value
            End Set
        End Property
        Private m_Y As Integer
    End Class

    Public Class XboxController
        Private _playerIndex As Integer
        Shared keepRunning As Boolean
        Shared m_updateFrequency As Integer
        Shared waitTime As Integer
        Shared isRunning As Boolean
        Shared [SyncLock] As Object
        Shared pollingThread As Thread

        Private _stopMotorTimerActive As Boolean
        Private _stopMotorTime As DateTime
        Private _batteryInformationGamepad As XInputBatteryInformation
        Private _batterInformationHeadset As XInputBatteryInformation
        'XInputCapabilities _capabilities;

        Private gamepadStatePrev As New XInputState()
        Private gamepadStateCurrent As New XInputState()

        Public Shared Property UpdateFrequency() As Integer
            Get
                Return m_updateFrequency
            End Get
            Set(value As Integer)
                m_updateFrequency = value
                waitTime = 1000 \ m_updateFrequency
            End Set
        End Property

        Public Property BatteryInformationGamepad() As XInputBatteryInformation
            Get
                Return _batteryInformationGamepad
            End Get
            Friend Set(value As XInputBatteryInformation)
                _batteryInformationGamepad = value
            End Set
        End Property

        Public Property BatteryInformationHeadset() As XInputBatteryInformation
            Get
                Return _batterInformationHeadset
            End Get
            Friend Set(value As XInputBatteryInformation)
                _batterInformationHeadset = value
            End Set
        End Property

        Public Const MAX_CONTROLLER_COUNT As Integer = 4
        Public Const FIRST_CONTROLLER_INDEX As Integer = 0
        Public Const LAST_CONTROLLER_INDEX As Integer = MAX_CONTROLLER_COUNT - 1

        Shared Controllers As XboxController()


        Shared Sub New()
            Controllers = New XboxController(MAX_CONTROLLER_COUNT - 1) {}
            [SyncLock] = New Object()
            For i As Integer = FIRST_CONTROLLER_INDEX To LAST_CONTROLLER_INDEX
                Controllers(i) = New XboxController(i)
            Next
            UpdateFrequency = 25
        End Sub

        Public Event StateChanged As EventHandler(Of XboxControllerStateChangedEventArgs)

        Public Shared Function RetrieveController(index As Integer) As XboxController
            Return Controllers(index)
        End Function

        Private Sub New(playerIndex As Integer)
            _playerIndex = playerIndex
            gamepadStatePrev.Copy(gamepadStateCurrent)
        End Sub

        Public Sub UpdateBatteryState()
            Dim headset As New XInputBatteryInformation(), gamepad As New XInputBatteryInformation()

            XInput.XInputGetBatteryInformation(_playerIndex, CByte(BatteryDeviceType.BATTERY_DEVTYPE_GAMEPAD), gamepad)
            XInput.XInputGetBatteryInformation(_playerIndex, CByte(BatteryDeviceType.BATTERY_DEVTYPE_HEADSET), headset)

            BatteryInformationHeadset = headset
            BatteryInformationGamepad = gamepad
        End Sub

        Protected Sub OnStateChanged()
            Dim temp = New XboxControllerStateChangedEventArgs()

            temp.CurrentInputState = gamepadStateCurrent
            temp.PreviousInputState = gamepadStatePrev

            RaiseEvent StateChanged(Me, temp)
        End Sub

        Public Function GetCapabilities() As XInputCapabilities
            Dim capabilities As New XInputCapabilities()
            XInput.XInputGetCapabilities(_playerIndex, XInputConstants.XINPUT_FLAG_GAMEPAD, capabilities)
            Return capabilities
        End Function


#Region "Digital Button States"
        Public ReadOnly Property IsDPadUpPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_DPAD_UP))
            End Get
        End Property

        Public ReadOnly Property IsDPadDownPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_DPAD_DOWN))
            End Get
        End Property

        Public ReadOnly Property IsDPadLeftPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_DPAD_LEFT))
            End Get
        End Property

        Public ReadOnly Property IsDPadRightPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_DPAD_RIGHT))
            End Get
        End Property

        Public ReadOnly Property IsAPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_A))
            End Get
        End Property

        Public ReadOnly Property IsBPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_B))
            End Get
        End Property

        Public ReadOnly Property IsXPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_X))
            End Get
        End Property

        Public ReadOnly Property IsYPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_Y))
            End Get
        End Property


        Public ReadOnly Property IsBackPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_BACK))
            End Get
        End Property


        Public ReadOnly Property IsStartPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_START))
            End Get
        End Property


        Public ReadOnly Property IsLeftShoulderPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_LEFT_SHOULDER))
            End Get
        End Property


        Public ReadOnly Property IsRightShoulderPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_RIGHT_SHOULDER))
            End Get
        End Property

        Public ReadOnly Property IsLeftStickPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_LEFT_THUMB))
            End Get
        End Property

        Public ReadOnly Property IsRightStickPressed() As Boolean
            Get
                Return gamepadStateCurrent.Gamepad.IsButtonPressed(CInt(ButtonFlags.XINPUT_GAMEPAD_RIGHT_THUMB))
            End Get
        End Property
#End Region

#Region "Analogue Input States"
        Public ReadOnly Property LeftTrigger() As Integer
            Get
                Return CInt(gamepadStateCurrent.Gamepad.bLeftTrigger)
            End Get
        End Property

        Public ReadOnly Property RightTrigger() As Integer
            Get
                Return CInt(gamepadStateCurrent.Gamepad.bRightTrigger)
            End Get
        End Property

        Public ReadOnly Property LeftThumbStick() As Point
            Get

                Dim p As New Point()

                p.X = gamepadStateCurrent.Gamepad.sThumbLX
                p.Y = gamepadStateCurrent.Gamepad.sThumbLY

                Return p
            End Get
        End Property

        Public ReadOnly Property RightThumbStick() As Point
            Get
                Dim p As New Point()
                p.X = gamepadStateCurrent.Gamepad.sThumbRX
                p.Y = gamepadStateCurrent.Gamepad.sThumbRY

                Return p
            End Get
        End Property

#End Region

        Private _isConnected As Boolean
        Public Property IsConnected() As Boolean
            Get
                Return _isConnected
            End Get
            Friend Set(value As Boolean)
                _isConnected = value
            End Set
        End Property

#Region "Polling"
        Public Shared Sub StartPolling()
            If Not isRunning Then
                SyncLock [SyncLock]
                    If Not isRunning Then
                        pollingThread = New Thread(AddressOf PollerLoop)
                        pollingThread.Start()
                    End If
                End SyncLock
            End If
        End Sub

        Public Shared Sub StopPolling()
            If isRunning Then
                keepRunning = False
            End If
        End Sub

        Private Shared Sub PollerLoop()
            SyncLock [SyncLock]
                If isRunning = True Then
                    Return
                End If
                isRunning = True
            End SyncLock
            keepRunning = True
            While keepRunning
                For i As Integer = FIRST_CONTROLLER_INDEX To LAST_CONTROLLER_INDEX
                    Controllers(i).UpdateState()
                Next
                Thread.Sleep(m_updateFrequency)
            End While
            SyncLock [SyncLock]
                isRunning = False
            End SyncLock
        End Sub

        Public Sub UpdateState()
            Dim X As New XInputCapabilities()
            Dim result As Integer = XInput.XInputGetState(_playerIndex, gamepadStateCurrent)
            IsConnected = (result = 0)

            UpdateBatteryState()
            If gamepadStateCurrent.PacketNumber <> gamepadStatePrev.PacketNumber Then
                OnStateChanged()
            End If
            gamepadStatePrev.Copy(gamepadStateCurrent)

            If _stopMotorTimerActive AndAlso (DateTime.Now >= _stopMotorTime) Then
                Dim stopStrength As New XInputVibration() 'With { _
                stopStrength.LeftMotorSpeed = 0
                stopStrength.RightMotorSpeed = 0

                XInput.XInputSetState(_playerIndex, stopStrength)
            End If
        End Sub
#End Region

#Region "Motor Functions"
        Public Sub Vibrate(leftMotor As Double, rightMotor As Double)
            Vibrate(leftMotor, rightMotor, TimeSpan.MinValue)
        End Sub

        Public Sub Vibrate(leftMotor As Double, rightMotor As Double, length As TimeSpan)
            leftMotor = Math.Max(0.0, Math.Min(1.0, leftMotor))
            rightMotor = Math.Max(0.0, Math.Min(1.0, rightMotor))

            Dim vibration As New XInputVibration()
            vibration.LeftMotorSpeed = CUShort(Math.Truncate(65535.0 * leftMotor))
            vibration.RightMotorSpeed = CUShort(Math.Truncate(65535.0 * rightMotor))

            Vibrate(vibration, length)
        End Sub


        Public Sub Vibrate(strength As XInputVibration)
            _stopMotorTimerActive = False
            XInput.XInputSetState(_playerIndex, strength)
        End Sub

        Public Sub Vibrate(strength As XInputVibration, length As TimeSpan)
            XInput.XInputSetState(_playerIndex, strength)
            If length <> TimeSpan.MinValue Then
                _stopMotorTime = DateTime.Now.Add(length)
                _stopMotorTimerActive = True
            End If
        End Sub
#End Region

        Public Overrides Function ToString() As String
            Return _playerIndex.ToString()
        End Function

    End Class

    Public Class XboxControllerStateChangedEventArgs
        Inherits EventArgs
        Public Property CurrentInputState() As XInputState
            Get
                Return m_CurrentInputState
            End Get
            Set(value As XInputState)
                m_CurrentInputState = Value
            End Set
        End Property
        Private m_CurrentInputState As XInputState
        Public Property PreviousInputState() As XInputState
            Get
                Return m_PreviousInputState
            End Get
            Set(value As XInputState)
                m_PreviousInputState = Value
            End Set
        End Property
        Private m_PreviousInputState As XInputState
    End Class

    Public NotInheritable Class XInput
        Private Sub New()
        End Sub
#If WINDOWS7 Then
		' [in] Index of the gamer associated with the device
		<DllImport("xinput9_1_0.dll")> _
		Public Shared Function XInputGetState(dwUserIndex As Integer, ByRef pState As XInputState) As Integer
			' [out] Receives the current state
		End Function

		' [in] Index of the gamer associated with the device
		<DllImport("xinput9_1_0.dll")> _
		Public Shared Function XInputSetState(dwUserIndex As Integer, ByRef pVibration As XInputVibration) As Integer
			' [in, out] The vibration information to send to the controller
		End Function

		' [in] Index of the gamer associated with the device
		' [in] Input flags that identify the device type
		<DllImport("xinput9_1_0.dll")> _
		Public Shared Function XInputGetCapabilities(dwUserIndex As Integer, dwFlags As Integer, ByRef pCapabilities As XInputCapabilities) As Integer
			' [out] Receives the capabilities
		End Function


		'this function is not available prior to Windows 8
		' Index of the gamer associated with the device
		' Which device on this user index
		Public Shared Function XInputGetBatteryInformation(dwUserIndex As Integer, devType As Byte, ByRef pBatteryInformation As XInputBatteryInformation) As Integer
		' Contains the level and types of batteries
			Return 0
		End Function

		'this function is not available prior to Windows 8
		' Index of the gamer associated with the device
		' Reserved for future use
		Public Shared Function XInputGetKeystroke(dwUserIndex As Integer, dwReserved As Integer, ByRef pKeystroke As XInputKeystroke) As Integer
		' Pointer to an XINPUT_KEYSTROKE structure that receives an input event.
			Return 0
		End Function
#Else


        ' [in] Index of the gamer associated with the device
        <DllImport("xinput1_4.dll")> _
        Public Shared Function XInputGetState(dwUserIndex As Integer, ByRef pState As XInputState) As Integer
            ' [out] Receives the current state
        End Function

        ' [in] Index of the gamer associated with the device
        <DllImport("xinput1_4.dll")> _
        Public Shared Function XInputSetState(dwUserIndex As Integer, ByRef pVibration As XInputVibration) As Integer
            ' [in, out] The vibration information to send to the controller
        End Function

        ' [in] Index of the gamer associated with the device
        ' [in] Input flags that identify the device type
        <DllImport("xinput1_4.dll")> _
        Public Shared Function XInputGetCapabilities(dwUserIndex As Integer, dwFlags As Integer, ByRef pCapabilities As XInputCapabilities) As Integer
            ' [out] Receives the capabilities
        End Function


        ' Index of the gamer associated with the device
        ' Which device on this user index
        <DllImport("xinput1_4.dll")> _
        Public Shared Function XInputGetBatteryInformation(dwUserIndex As Integer, devType As Byte, ByRef pBatteryInformation As XInputBatteryInformation) As Integer
            ' Contains the level and types of batteries
        End Function

        ' Index of the gamer associated with the device
        ' Reserved for future use
        <DllImport("xinput1_4.dll")> _
        Public Shared Function XInputGetKeystroke(dwUserIndex As Integer, dwReserved As Integer, ByRef pKeystroke As XInputKeystroke) As Integer
            ' Pointer to an XINPUT_KEYSTROKE structure that receives an input event.
        End Function
#End If
    End Class


End Namespace
