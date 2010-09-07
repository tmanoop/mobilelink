Imports System.Net
Imports System.IO
Imports System.Net.Sockets
Imports System.IO.Ports.SerialPort
Imports InTheHand.Net.Sockets
Imports InTheHand.Net.Bluetooth
Imports InTheHand.Net

Public Class Form1
    Inherits System.Windows.Forms.Form

    Const MAX_MESSAGE_SIZE As Integer = 128
    Const MAX_TRIES As Integer = 3

    Public ServiceName As New Guid("{E075D486-E23D-4887-8AF5-DAA1F6A5B172}")

    Dim btClient As New BluetoothClient
    Dim btListener As BluetoothListener

    Public listening As Boolean = True

    'holds the incoming message
    Dim str As String

    Public Sub sendMessage(ByVal NumRetries As Integer, _
                            ByVal Buffer() As Byte, _
                            ByVal BufferLen As Integer)


        Dim client As BluetoothClient = Nothing
        Dim CurrentTries As Integer = 0
        Do
            Try
                client = New BluetoothClient
                client.Connect(New BluetoothEndPoint(CType(cboDevices.SelectedItem, BluetoothDeviceInfo).DeviceAddress, ServiceName))


            Catch se As SocketException
                If (CurrentTries >= NumRetries) Then
                    Throw se
                End If
            End Try
            CurrentTries = CurrentTries + 1

        Loop While client Is Nothing And _
             CurrentTries < NumRetries

        If (client Is Nothing) Then
            'timeout occurred
            MsgBox("Error establishing contact")
            Return
        End If

        Dim stream As System.IO.Stream = Nothing
        Try
            stream = client.GetStream()
            stream.Write(Buffer, 0, BufferLen)
        Catch e As Exception
            MsgBox("Error sending")
        Finally
            If (Not stream Is Nothing) Then
                stream.Close()
            End If
            If (Not client Is Nothing) Then
                client.Close()
            End If
        End Try
    End Sub

    Public Function receiveMessage(ByVal BufferLen As Integer) _
        As String
        Dim bytesRead As Integer = 0
        Dim client As BluetoothClient = Nothing
        Dim stream As System.IO.Stream = Nothing
        Dim Buffer(MAX_MESSAGE_SIZE) As Byte

        Try

            client = btListener.AcceptBluetoothClient()  ' blocking call
            stream = client.GetStream()
            bytesRead = stream.Read(Buffer, 0, BufferLen)

            str = client.RemoteMachineName + "->" + _
                    System.Text.Encoding.Unicode.GetString(Buffer, 0, bytesRead) + vbCrLf

        Catch e As Exception
            'dont display error if we are ending the listener
            If listening Then
                MsgBox("Error listening to incoming message")
            End If

        Finally
            If (Not stream Is Nothing) Then
                stream.Close()
            End If
            If (Not client Is Nothing) Then
                client.Close()
            End If

        End Try
        Return str
    End Function

    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim t1 As System.Threading.Thread
        t1 = New Threading.Thread(AddressOf receiveLoop)
        t1.Start()

        btClient = New BluetoothClient


        Dim bdi As BluetoothDeviceInfo() = btClient.DiscoverDevices(6)

        cboDevices.DataSource = bdi
        cboDevices.DisplayMember = "DeviceName"

    End Sub
    Public Sub receiveLoop()
        Dim strReceived As String
        btListener = New BluetoothListener(ServiceName)
        btListener.Start()

        strReceived = receiveMessage(MAX_MESSAGE_SIZE)
        While listening '---keep on listening for new message
            If strReceived <> "" Then
                Me.Invoke(New EventHandler(AddressOf UpdateTextBox))

                strReceived = receiveMessage(MAX_MESSAGE_SIZE)
            End If
        End While

    End Sub


    Public Sub UpdateTextBox(ByVal sender As Object, ByVal e As EventArgs)
        '---delegate to update the textbox control
        txtMessagesArchive.Text += str
        'call lca client code to send verification msg as 
        'text = verification: 01,02,12,999,0'
        'call lcaSendFunction(text)
    End Sub

    Protected Friend Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        'stop receive loop
        listening = False
        'stop listening service
        btListener.Stop()

        Application.Exit()


    End Sub


    Public Sub mnuSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSend.Click, MenuItem3.Click
        'hard code below text as string(claim :01,12,999,0)
        sendMessage(MAX_TRIES, _
                    System.Text.Encoding.Unicode.GetBytes(txtmessage.Text), _
                    txtmessage.Text.Length * 2)
        'text = claim: 01,12,999,0'
        'call lcaSendFunction(text)
    End Sub

    Public Sub mnuSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSearch.Click
        Cursor.Current = Cursors.WaitCursor

        Dim bdi As BluetoothDeviceInfo() = btClient.DiscoverDevices(6)
        cboDevices.DataSource = bdi

        Cursor.Current = Cursors.Default
    End Sub

    Public Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
    End Sub

    Public Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        'text = claim coupon service
        'call lbsSereverFunction(text) using vb client code.
        'recieve message from LBS and show it on the mobile popup.

        Dim tcpClient As New System.Net.Sockets.TcpClient()
        tcpClient.Connect("128.235.64.175", 8000)
        Dim networkStream As NetworkStream = tcpClient.GetStream()
        If networkStream.CanWrite And networkStream.CanRead Then
            ' Do a simple write.
            Dim sendBytes As [Byte]() = Encoding.GetBytes("request coupon service")
            networkStream.Write(sendBytes, 0, sendBytes.Length)
            ' Read the NetworkStream into a byte buffer.
            Dim bytes(tcpClient.ReceiveBufferSize) As Byte
            networkStream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
            ' Output the data received from the host to the console.
            Dim returndata As String = Encoding.ASCII.GetString(bytes, index:=2, count:=3)
            Console.WriteLine(("Host returned: " + returndata))
        Else
            If Not networkStream.CanRead Then
                Console.WriteLine("cannot not write data to this stream")
                tcpClient.Close()
            Else
                If Not networkStream.CanWrite Then
                    Console.WriteLine("cannot read data from this stream")
                    tcpClient.Close()
                End If
            End If
        End If
        ' pause so user can view the console output
        Console.ReadLine()
    End Sub

End Class

