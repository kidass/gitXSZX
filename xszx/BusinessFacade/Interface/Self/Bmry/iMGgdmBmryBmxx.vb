Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGgdmBmryBmxx
    '
    ' 功能描述： 
    '     ggdm_bmry_bmxx.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgdmBmryBmxx
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strtxtZZDM As String                    'txtZZDM
        Private m_strtxtZZMC As String                    'txtZZMC
        Private m_strtxtZZBM As String                    'txtZZBM
        Private m_strtxtJBMC As String                    'txtJBMC
        Private m_strtxtMSMC As String                    'txtMSMC
        Private m_strtxtLXDH As String                    'txtLXDH
        Private m_strtxtSJHM As String                    'txtSJHM
        Private m_strtxtFTPDZ As String                   'txtFTPDZ
        Private m_strtxtYXDZ As String                    'txtYXDZ
        Private m_strtxtLXDZ As String                    'txtLXDZ
        Private m_strtxtYZBM As String                    'txtYZBM
        Private m_strtxtLXR As String                     'txtLXR

        Private m_strhtxtJBDM As String                   'htxtJBDM
        Private m_strhtxtMSDM As String                   'htxtMSDM
        Private m_strhtxtLXRDM As String                  'htxtLXRDM









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtZZDM = ""
            m_strtxtZZMC = ""
            m_strtxtZZBM = ""
            m_strtxtJBMC = ""
            m_strtxtMSMC = ""
            m_strtxtLXDH = ""
            m_strtxtSJHM = ""
            m_strtxtFTPDZ = ""
            m_strtxtYXDZ = ""
            m_strtxtLXDZ = ""
            m_strtxtYZBM = ""
            m_strtxtLXR = ""

            m_strhtxtJBDM = ""
            m_strhtxtMSDM = ""
            m_strhtxtLXRDM = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgdmBmryBmxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' txtZZDM属性
        '----------------------------------------------------------------
        Public Property txtZZDM() As String
            Get
                txtZZDM = m_strtxtZZDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZZDM = Value
                Catch ex As Exception
                    m_strtxtZZDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtZZMC属性
        '----------------------------------------------------------------
        Public Property txtZZMC() As String
            Get
                txtZZMC = m_strtxtZZMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZZMC = Value
                Catch ex As Exception
                    m_strtxtZZMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtZZBM属性
        '----------------------------------------------------------------
        Public Property txtZZBM() As String
            Get
                txtZZBM = m_strtxtZZBM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZZBM = Value
                Catch ex As Exception
                    m_strtxtZZBM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJBMC属性
        '----------------------------------------------------------------
        Public Property txtJBMC() As String
            Get
                txtJBMC = m_strtxtJBMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJBMC = Value
                Catch ex As Exception
                    m_strtxtJBMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJBDM属性
        '----------------------------------------------------------------
        Public Property htxtJBDM() As String
            Get
                htxtJBDM = m_strhtxtJBDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJBDM = Value
                Catch ex As Exception
                    m_strhtxtJBDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtMSMC属性
        '----------------------------------------------------------------
        Public Property txtMSMC() As String
            Get
                txtMSMC = m_strtxtMSMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtMSMC = Value
                Catch ex As Exception
                    m_strtxtMSMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtMSDM属性
        '----------------------------------------------------------------
        Public Property htxtMSDM() As String
            Get
                htxtMSDM = m_strhtxtMSDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtMSDM = Value
                Catch ex As Exception
                    m_strhtxtMSDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLXDH属性
        '----------------------------------------------------------------
        Public Property txtLXDH() As String
            Get
                txtLXDH = m_strtxtLXDH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLXDH = Value
                Catch ex As Exception
                    m_strtxtLXDH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJHM属性
        '----------------------------------------------------------------
        Public Property txtSJHM() As String
            Get
                txtSJHM = m_strtxtSJHM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJHM = Value
                Catch ex As Exception
                    m_strtxtSJHM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFTPDZ属性
        '----------------------------------------------------------------
        Public Property txtFTPDZ() As String
            Get
                txtFTPDZ = m_strtxtFTPDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFTPDZ = Value
                Catch ex As Exception
                    m_strtxtFTPDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYXDZ属性
        '----------------------------------------------------------------
        Public Property txtYXDZ() As String
            Get
                txtYXDZ = m_strtxtYXDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYXDZ = Value
                Catch ex As Exception
                    m_strtxtYXDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLXDZ属性
        '----------------------------------------------------------------
        Public Property txtLXDZ() As String
            Get
                txtLXDZ = m_strtxtLXDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLXDZ = Value
                Catch ex As Exception
                    m_strtxtLXDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYZBM属性
        '----------------------------------------------------------------
        Public Property txtYZBM() As String
            Get
                txtYZBM = m_strtxtYZBM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYZBM = Value
                Catch ex As Exception
                    m_strtxtYZBM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLXR属性
        '----------------------------------------------------------------
        Public Property txtLXR() As String
            Get
                txtLXR = m_strtxtLXR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLXR = Value
                Catch ex As Exception
                    m_strtxtLXR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLXRDM属性
        '----------------------------------------------------------------
        Public Property htxtLXRDM() As String
            Get
                htxtLXRDM = m_strhtxtLXRDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLXRDM = Value
                Catch ex As Exception
                    m_strhtxtLXRDM = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
