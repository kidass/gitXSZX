Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGgxxLdapInfo
    '
    ' 功能描述： 
    '     ggxx_ldap_info.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxLdapInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strtxtAPRQ As String                      'txtAPRQ
        Private m_strtxtXH As String                        'txtXH
        Private m_strtxtPX As String                        'txtPX
        Private m_strtxtCJLD As String                      'txtCJLD
        Private m_strtxtKSSJ As String                      'txtKSSJ
        Private m_strtxtJSSJ As String                      'txtJSSJ
        Private m_strtxtDD As String                        'txtDD
        Private m_strtxtHDNR As String                      'txtHDNR
        Private m_strtxtBZ As String                        'txtBZ
        Private m_strtxtSJ As String                        'txtSJ

        Private m_strhtxtAPSJ As String                     'htxtAPSJ

        Private m_intSelectedIndex_ddlSJ As Integer          'ddlsj

        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain













        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtAPRQ = ""
            m_strtxtXH = ""
            m_strtxtPX = ""
            m_strtxtCJLD = ""
            m_strtxtKSSJ = ""
            m_strtxtJSSJ = ""
            m_strtxtDD = ""
            m_strtxtHDNR = ""
            m_strtxtBZ = ""
            m_strtxtSJ = ""

            m_strhtxtAPSJ = ""

            m_intSelectedIndex_ddlSJ = -1

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""
            m_strhtxtDivLeftMain = ""
            m_strhtxtDivTopMain = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxLdapInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub















        '----------------------------------------------------------------
        ' txtAPRQ属性
        '----------------------------------------------------------------
        Public Property txtAPRQ() As String
            Get
                txtAPRQ = m_strtxtAPRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtAPRQ = Value
                Catch ex As Exception
                    m_strtxtAPRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtPX属性
        '----------------------------------------------------------------
        Public Property txtPX() As String
            Get
                txtPX = m_strtxtPX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtPX = Value
                Catch ex As Exception
                    m_strtxtPX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDD属性
        '----------------------------------------------------------------
        Public Property txtDD() As String
            Get
                txtDD = m_strtxtDD
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDD = Value
                Catch ex As Exception
                    m_strtxtDD = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtHDNR属性
        '----------------------------------------------------------------
        Public Property txtHDNR() As String
            Get
                txtHDNR = m_strtxtHDNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtHDNR = Value
                Catch ex As Exception
                    m_strtxtHDNR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtXH属性
        '----------------------------------------------------------------
        Public Property txtXH() As String
            Get
                txtXH = m_strtxtXH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXH = Value
                Catch ex As Exception
                    m_strtxtXH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtKSSJ属性
        '----------------------------------------------------------------
        Public Property txtKSSJ() As String
            Get
                txtKSSJ = m_strtxtKSSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtKSSJ = Value
                Catch ex As Exception
                    m_strtxtKSSJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSSJ属性
        '----------------------------------------------------------------
        Public Property txtJSSJ() As String
            Get
                txtJSSJ = m_strtxtJSSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSSJ = Value
                Catch ex As Exception
                    m_strtxtJSSJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtCJLD属性
        '----------------------------------------------------------------
        Public Property txtCJLD() As String
            Get
                txtCJLD = m_strtxtCJLD
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCJLD = Value
                Catch ex As Exception
                    m_strtxtCJLD = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBZ属性
        '----------------------------------------------------------------
        Public Property txtBZ() As String
            Get
                txtBZ = m_strtxtBZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBZ = Value
                Catch ex As Exception
                    m_strtxtBZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJ属性
        '----------------------------------------------------------------
        Public Property txtSJ() As String
            Get
                txtSJ = m_strtxtSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJ = Value
                Catch ex As Exception
                    m_strtxtSJ = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtAPSJ属性
        '----------------------------------------------------------------
        Public Property htxtAPSJ() As String
            Get
                htxtAPSJ = m_strhtxtAPSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtAPSJ = Value
                Catch ex As Exception
                    m_strhtxtAPSJ = ""
                End Try
            End Set
        End Property




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
        ' htxtDivLeftMain属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftMain() As String
            Get
                htxtDivLeftMain = m_strhtxtDivLeftMain
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftMain = Value
                Catch ex As Exception
                    m_strhtxtDivLeftMain = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopMain属性
        '----------------------------------------------------------------
        Public Property htxtDivTopMain() As String
            Get
                htxtDivTopMain = m_strhtxtDivTopMain
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopMain = Value
                Catch ex As Exception
                    m_strhtxtDivTopMain = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' ddlSJ_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlSJ_SelectedIndex() As Integer
            Get
                ddlSJ_SelectedIndex = m_intSelectedIndex_ddlSJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlSJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlSJ = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
