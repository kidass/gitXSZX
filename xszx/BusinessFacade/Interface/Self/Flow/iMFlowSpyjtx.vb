Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowSpyjtx
    '
    ' 功能描述： 
    '     flow_spyjtx.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowSpyjtx
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSFPZ As String                           'htxtSFPZ
        Private m_strhtxtJJXH As String                           'htxtJJXH
        Private m_strhtxtYJLX As String                           'htxtYJLX
        Private m_strhtxtValueA As String                         'htxtValueA
        Private m_strhtxtValueB As String                         'htxtValueB
        Private m_strhtxtValueC As String                         'htxtValueC
        Private m_strhtxtLastYJLX As String                     'htxtLastYJLX

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtFSR As String                            'txtFSR
        Private m_strtxtSPR As String                            'txtSPR
        Private m_strtxtBDR As String                            'txtBDR
        Private m_strtxtSCSPSJ As String                         'txtSCSPSJ
        Private m_strtxtBDSJ As String                           'txtBDSJ
        Private m_strtxtSCSPLX As String                         'txtSCSPLX
        Private m_strtxtLDPSSJ As String                         'txtLDPSSJ

        Private m_blnChecked_chkXBBZ As Boolean                  'chkXBBZ

        '----------------------------------------------------------------
        'textarea
        '----------------------------------------------------------------
        Private m_strtextareaZSYJ As String                      'textareaZSYJ
        Private m_strtextareaBJYJ As String                      'textareaBJYJ
        Private m_strtextareaXZCKRY As String                    'textareaXZCKRY

        '----------------------------------------------------------------
        'dropdownlist
        '----------------------------------------------------------------
        Private m_intSelectedIndex_ddlLDMC As Integer            'ddlLDMC_SelectedIndex
        Private m_blnEnabled_ddlLDMC As Boolean                  'ddlLDMC_Enabled

        '----------------------------------------------------------------
        'radiobuttonlist
        '----------------------------------------------------------------
        Private m_intSelectedIndex_rblYJLX As Integer            'rblYJLX_SelectedIndex

        '----------------------------------------------------------------
        'radiobuttonlist
        '----------------------------------------------------------------
        Private m_blnEnabled_btnZuofei As Boolean                'btnZuofei_Enabled











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSFPZ = ""
            m_strhtxtJJXH = ""
            m_strhtxtYJLX = ""
            m_strhtxtValueA = ""
            m_strhtxtValueB = ""
            m_strhtxtValueC = ""
            m_strhtxtLastYJLX = ""

            'textbox
            m_strtxtFSR = ""
            m_strtxtSPR = ""
            m_strtxtBDR = ""
            m_strtxtSCSPSJ = ""
            m_strtxtBDSJ = ""
            m_strtxtSCSPLX = ""
            m_strtxtLDPSSJ = ""

            m_strtextareaZSYJ = ""
            m_strtextareaBJYJ = ""
            m_strtextareaXZCKRY = ""

            m_intSelectedIndex_ddlLDMC = -1
            m_intSelectedIndex_rblYJLX = -1

            m_blnEnabled_btnZuofei = False
            m_blnEnabled_ddlLDMC = False
            m_blnChecked_chkXBBZ = False

        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowSpyjtx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtDivLeftBody属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftBody() As String
            Get
                htxtDivLeftBody = m_strhtxtDivLeftBody
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftBody = Value
                Catch ex As Exception
                    m_strhtxtDivLeftBody = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopBody属性
        '----------------------------------------------------------------
        Public Property htxtDivTopBody() As String
            Get
                htxtDivTopBody = m_strhtxtDivTopBody
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopBody = Value
                Catch ex As Exception
                    m_strhtxtDivTopBody = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtSFPZ属性
        '----------------------------------------------------------------
        Public Property htxtSFPZ() As String
            Get
                htxtSFPZ = m_strhtxtSFPZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSFPZ = Value
                Catch ex As Exception
                    m_strhtxtSFPZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJJXH属性
        '----------------------------------------------------------------
        Public Property htxtJJXH() As String
            Get
                htxtJJXH = m_strhtxtJJXH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJJXH = Value
                Catch ex As Exception
                    m_strhtxtJJXH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYJLX属性
        '----------------------------------------------------------------
        Public Property htxtYJLX() As String
            Get
                htxtYJLX = m_strhtxtYJLX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYJLX = Value
                Catch ex As Exception
                    m_strhtxtYJLX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtValueA属性
        '----------------------------------------------------------------
        Public Property htxtValueA() As String
            Get
                htxtValueA = m_strhtxtValueA
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtValueA = Value
                Catch ex As Exception
                    m_strhtxtValueA = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtValueB属性
        '----------------------------------------------------------------
        Public Property htxtValueB() As String
            Get
                htxtValueB = m_strhtxtValueB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtValueB = Value
                Catch ex As Exception
                    m_strhtxtValueB = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtValueC属性
        '----------------------------------------------------------------
        Public Property htxtValueC() As String
            Get
                htxtValueC = m_strhtxtValueC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtValueC = Value
                Catch ex As Exception
                    m_strhtxtValueC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLastYJLX属性
        '----------------------------------------------------------------
        Public Property htxtLastYJLX() As String
            Get
                htxtLastYJLX = m_strhtxtLastYJLX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLastYJLX = Value
                Catch ex As Exception
                    m_strhtxtLastYJLX = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' txtFSR属性
        '----------------------------------------------------------------
        Public Property txtFSR() As String
            Get
                txtFSR = m_strtxtFSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFSR = Value
                Catch ex As Exception
                    m_strtxtFSR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSPR属性
        '----------------------------------------------------------------
        Public Property txtSPR() As String
            Get
                txtSPR = m_strtxtSPR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSPR = Value
                Catch ex As Exception
                    m_strtxtSPR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBDR属性
        '----------------------------------------------------------------
        Public Property txtBDR() As String
            Get
                txtBDR = m_strtxtBDR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBDR = Value
                Catch ex As Exception
                    m_strtxtBDR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSCSPSJ属性
        '----------------------------------------------------------------
        Public Property txtSCSPSJ() As String
            Get
                txtSCSPSJ = m_strtxtSCSPSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSCSPSJ = Value
                Catch ex As Exception
                    m_strtxtSCSPSJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBDSJ属性
        '----------------------------------------------------------------
        Public Property txtBDSJ() As String
            Get
                txtBDSJ = m_strtxtBDSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBDSJ = Value
                Catch ex As Exception
                    m_strtxtBDSJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSCSPLX属性
        '----------------------------------------------------------------
        Public Property txtSCSPLX() As String
            Get
                txtSCSPLX = m_strtxtSCSPLX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSCSPLX = Value
                Catch ex As Exception
                    m_strtxtSCSPLX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLDPSSJ属性
        '----------------------------------------------------------------
        Public Property txtLDPSSJ() As String
            Get
                txtLDPSSJ = m_strtxtLDPSSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLDPSSJ = Value
                Catch ex As Exception
                    m_strtxtLDPSSJ = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' textareaZSYJ属性
        '----------------------------------------------------------------
        Public Property textareaZSYJ() As String
            Get
                textareaZSYJ = m_strtextareaZSYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtextareaZSYJ = Value
                Catch ex As Exception
                    m_strtextareaZSYJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' textareaBJYJ属性
        '----------------------------------------------------------------
        Public Property textareaBJYJ() As String
            Get
                textareaBJYJ = m_strtextareaBJYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtextareaBJYJ = Value
                Catch ex As Exception
                    m_strtextareaBJYJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' textareaXZCKRY属性
        '----------------------------------------------------------------
        Public Property textareaXZCKRY() As String
            Get
                textareaXZCKRY = m_strtextareaXZCKRY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtextareaXZCKRY = Value
                Catch ex As Exception
                    m_strtextareaXZCKRY = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' ddlLDMC_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlLDMC_SelectedIndex() As Integer
            Get
                ddlLDMC_SelectedIndex = m_intSelectedIndex_ddlLDMC
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlLDMC = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlLDMC = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlLDMC_Enabled属性
        '----------------------------------------------------------------
        Public Property ddlLDMC_Enabled() As Boolean
            Get
                ddlLDMC_Enabled = m_blnEnabled_ddlLDMC
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnEnabled_ddlLDMC = Value
                Catch ex As Exception
                    m_blnEnabled_ddlLDMC = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' chkXBBZ属性
        '----------------------------------------------------------------
        Public Property chkXBBZ() As Boolean
            Get
                chkXBBZ = m_blnChecked_chkXBBZ
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnChecked_chkXBBZ = Value
                Catch ex As Exception
                    m_blnChecked_chkXBBZ = False
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' rblYJLX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property rblYJLX_SelectedIndex() As Integer
            Get
                rblYJLX_SelectedIndex = m_intSelectedIndex_rblYJLX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblYJLX = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblYJLX = 0
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' btnZuofei_Enabled属性
        '----------------------------------------------------------------
        Public Property btnZuofei_Enabled() As Boolean
            Get
                btnZuofei_Enabled = m_blnEnabled_btnZuofei
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnEnabled_btnZuofei = Value
                Catch ex As Exception
                    m_blnEnabled_btnZuofei = False
                End Try
            End Set
        End Property

    End Class

End Namespace
