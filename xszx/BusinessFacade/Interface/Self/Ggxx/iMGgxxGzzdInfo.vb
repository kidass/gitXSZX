Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGgxxGzzdInfo
    '
    ' 功能描述： 
    '     ggxx_gzzd_info.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxGzzdInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strtxtBT As String                        'txtBT
        Private m_strtxtFBDW As String                      'txtFBDW
        Private m_strtxtNR As String                        'txtNR
        Private m_strtxtBH As String                        'txtBH
        Private m_strtxtFBRQ As String                      'txtFBRQ
        Private m_strtxtPXH As String                       'txtPXH

        Private m_strhtxtJB As String                       'htxtJB
        Private m_strhtxtSJBH As String                     'htxtSJBH
        Private m_strhtxtWYBS As String                     'htxtWYBS

        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain













        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtBT = ""
            m_strtxtFBDW = ""
            m_strtxtNR = ""
            m_strtxtBH = ""
            m_strtxtFBRQ = ""
            m_strtxtPXH = ""

            m_strhtxtJB = ""
            m_strhtxtSJBH = ""
            m_strhtxtWYBS = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxGzzdInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' txtBT属性
        '----------------------------------------------------------------
        Public Property txtBT() As String
            Get
                txtBT = m_strtxtBT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBT = Value
                Catch ex As Exception
                    m_strtxtBT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFBDW属性
        '----------------------------------------------------------------
        Public Property txtFBDW() As String
            Get
                txtFBDW = m_strtxtFBDW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFBDW = Value
                Catch ex As Exception
                    m_strtxtFBDW = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNR属性
        '----------------------------------------------------------------
        Public Property txtNR() As String
            Get
                txtNR = m_strtxtNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNR = Value
                Catch ex As Exception
                    m_strtxtNR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBH属性
        '----------------------------------------------------------------
        Public Property txtBH() As String
            Get
                txtBH = m_strtxtBH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBH = Value
                Catch ex As Exception
                    m_strtxtBH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFBRQ属性
        '----------------------------------------------------------------
        Public Property txtFBRQ() As String
            Get
                txtFBRQ = m_strtxtFBRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFBRQ = Value
                Catch ex As Exception
                    m_strtxtFBRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtPXH属性
        '----------------------------------------------------------------
        Public Property txtPXH() As String
            Get
                txtPXH = m_strtxtPXH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtPXH = Value
                Catch ex As Exception
                    m_strtxtPXH = ""
                End Try
            End Set
        End Property






        '----------------------------------------------------------------
        ' htxtJB属性
        '----------------------------------------------------------------
        Public Property htxtJB() As String
            Get
                htxtJB = m_strhtxtJB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJB = Value
                Catch ex As Exception
                    m_strhtxtJB = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSJBH属性
        '----------------------------------------------------------------
        Public Property htxtSJBH() As String
            Get
                htxtSJBH = m_strhtxtSJBH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSJBH = Value
                Catch ex As Exception
                    m_strhtxtSJBH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWYBS属性
        '----------------------------------------------------------------
        Public Property htxtWYBS() As String
            Get
                htxtWYBS = m_strhtxtWYBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWYBS = Value
                Catch ex As Exception
                    m_strhtxtWYBS = ""
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

    End Class

End Namespace
