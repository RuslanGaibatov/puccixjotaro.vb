Public Class Form1
    ' Lebenspunkte der Charaktere
    Dim jotaroHP As Integer = 100
    Dim pucciHP As Integer = 100

    ' Spielstatus
    Dim isTimeStopped As Boolean = False
    Dim isMadeInHeavenActive As Boolean = False

    ' Spielgeschwindigkeit (beeinflusst Verlangsamung/Beschleunigung)
    Dim gameSpeed As Integer = 100

    ' Schaden durch Angriffe
    Dim attackDamage As Integer = 10

    ' Beim Laden der Form
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialisierung der Anzeige der Lebenspunkte
        lblJotaroHP.Text = "Jotaro HP: " & jotaroHP
        LblPucciHP.Text = "Pucci HP: " & pucciHP

        ' Bilder der Charaktere
        PicJotaro.Image = Image.FromFile("400px-ASBR_JotaroSpecialActivate.png") ' Bild von Jotaro
        PicPucci.Image = Image.FromFile("PTN_Pucci_Stand.png") ' Bild von Pucci

        ' Timer starten
        gameTimer.Start()
    End Sub

    ' Timer zur Steuerung der Zeit
    Private Sub gameTimer_Tick(sender As Object, e As EventArgs) Handles gameTimer.Tick
        ' Wenn Made in Heaven aktiv ist, beschleunigen wir das Spiel
        If isMadeInHeavenActive Then
            gameSpeed = 50 ' Spiel beschleunigen
        Else
            gameSpeed = 100 ' Normale Spielgeschwindigkeit
        End If

        ' Wenn Time Stop aktiv ist, verlangsamen wir das Spiel für Jotaro
        If isTimeStopped Then
            gameSpeed = 200 ' Längere Dauer der Zeitverzögerung
        End If
    End Sub

    ' Bei Klick auf die Punch-Taste von Jotaro
    Private Sub btnPunch_Click(sender As Object, e As EventArgs) Handles btnPunch.Click
        ' Wenn die Zeit nicht gestoppt ist
        If Not isTimeStopped Then
            pucciHP -= attackDamage ' Reduziere die HP von Pucci
            LblPucciHP.Text = "Pucci HP: " & pucciHP
            CheckGameOver() ' Überprüfe, ob das Spiel vorbei ist
        Else
            ' Wenn Time Stop aktiv ist, verdoppeln wir den Schaden
            pucciHP -= attackDamage * 2 ' Schaden verdoppeln, wenn die Zeit gestoppt ist
            LblPucciHP.Text = "Pucci HP: " & pucciHP
            isTimeStopped = False ' Beende den Effekt von Time Stop
            CheckGameOver() ' Überprüfe, ob das Spiel vorbei ist
        End If
    End Sub

    ' Bei Klick auf die Time Stop-Taste von Jotaro
    Private Sub btnTimeStop_Click(sender As Object, e As EventArgs) Handles btnTimeStop.Click
        If jotaroHP > 0 Then
            isTimeStopped = True
            lblStatus.Text = "Die Zeit wurde gestoppt!"
            gameSpeed = 200 ' Verlangsame das Spiel während Time Stop
        End If
    End Sub

    ' Bei Klick auf die Made in Heaven-Taste von Pucci
    Private Sub btnMadeInHeaven_Click(sender As Object, e As EventArgs) Handles btnMadeInHeaven.Click
        If pucciHP > 0 Then
            isMadeInHeavenActive = True
            lblStatus.Text = "Made in Heaven aktiviert!"
        End If
    End Sub

    ' Bei Klick auf die Attack-Taste von Pucci
    Private Sub btnAttack_Click(sender As Object, e As EventArgs) Handles btnAttack.Click
        ' Angriff von Pucci
        jotaroHP -= attackDamage ' Reduziere die HP von Jotaro
        lblJotaroHP.Text = "Jotaro HP: " & jotaroHP
        CheckGameOver() ' Überprüfe, ob das Spiel vorbei ist
    End Sub

    ' Überprüft, ob das Spiel vorbei ist
    Private Sub CheckGameOver()
        If jotaroHP <= 0 Then
            lblStatus.Text = "Pucci hat gewonnen!"
            gameTimer.Stop() ' Stoppe den Timer und beende das Spiel
        ElseIf pucciHP <= 0 Then
            lblStatus.Text = "Jotaro hat gewonnen!"
            gameTimer.Stop() ' Stoppe den Timer und beende das Spiel
        End If
    End Sub
End Class
