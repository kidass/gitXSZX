Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IXtglSjdxSjkdx
    '
    ' 功能描述： 
    '     xtgl_sjdx_sjkdx.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IXtglSjdxSjkdx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '编辑模式
        Private m_strDXBS_I As String                     '查看、编辑、拷贝时用的对象标识
        Private m_strFWQMC_I As String                    '查看、编辑、拷贝时用的服务器名称
        Private m_strSJKMC_I As String                    '查看、编辑、拷贝时用的数据库名称
        Private m_strDXLX_I As String                     '查看、编辑、拷贝时用的对象类型
        Private m_strDXMC_I As String                     '查看、编辑、拷贝时用的对象名称

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '返回方式：True-确定，False-取消
        Private m_strDXBS_O As String                     '返回正在处理的对象标识
        Private m_strFWQMC_O As String                    '返回正在处理的服务器名称
        Private m_strSJKMC_O As String                    '返回正在处理的数据库名称
        Private m_strDXLX_O As String                     '返回正在处理的对象类型
        Private m_strDXMC_O As String                     '返回正在处理的对象名称










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
            m_strDXBS_I = ""
            m_strFWQMC_I = ""
            m_strSJKMC_I = ""
            m_strDXLX_I = ""
            m_strDXMC_I = ""

            '初始化输出参数
            m_blnExitMode_O = False
            m_strDXBS_O = ""
            m_strFWQMC_O = ""
            m_strSJKMC_O = ""
            m_strDXLX_O = ""
            m_strDXMC_O = ""

        End Sub

        '----------------------------------------------------------------
        ' 重载父类的析构函数
        '----------------------------------------------------------------
        Public Overloads Sub Dispose()
            MyBase.Dispose()
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
            '释放资源
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IXtglSjdxSjkdx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' iEditMode属性
        '----------------------------------------------------------------
        Public Property iEditMode() As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
            Get
                iEditMode = m_objEditMode_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType)
                Try
                    m_objEditMode_I = Value
                Catch ex As Exception
                    m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDXBS属性
        '----------------------------------------------------------------
        Public Property iDXBS() As String
            Get
                iDXBS = m_strDXBS_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXBS_I = Value
                Catch ex As Exception
                    m_strDXBS_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iFWQMC属性
        '----------------------------------------------------------------
        Public Property iFWQMC() As String
            Get
                iFWQMC = m_strFWQMC_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFWQMC_I = Value
                Catch ex As Exception
                    m_strFWQMC_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSJKMC属性
        '----------------------------------------------------------------
        Public Property iSJKMC() As String
            Get
                iSJKMC = m_strSJKMC_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJKMC_I = Value
                Catch ex As Exception
                    m_strSJKMC_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDXLX属性
        '----------------------------------------------------------------
        Public Property iDXLX() As String
            Get
                iDXLX = m_strDXLX_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXLX_I = Value
                Catch ex As Exception
                    m_strDXLX_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDXMC属性
        '----------------------------------------------------------------
        Public Property iDXMC() As String
            Get
                iDXMC = m_strDXMC_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXMC_I = Value
                Catch ex As Exception
                    m_strDXMC_I = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' oExitMode属性
        '----------------------------------------------------------------
        Public Property oExitMode() As Boolean
            Get
                oExitMode = m_blnExitMode_O
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnExitMode_O = Value
                Catch ex As Exception
                    m_blnExitMode_O = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oDXBS属性
        '----------------------------------------------------------------
        Public Property oDXBS() As String
            Get
                oDXBS = m_strDXBS_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXBS_O = Value
                Catch ex As Exception
                    m_strDXBS_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oFWQMC属性
        '----------------------------------------------------------------
        Public Property oFWQMC() As String
            Get
                oFWQMC = m_strFWQMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFWQMC_O = Value
                Catch ex As Exception
                    m_strFWQMC_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oSJKMC属性
        '----------------------------------------------------------------
        Public Property oSJKMC() As String
            Get
                oSJKMC = m_strSJKMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJKMC_O = Value
                Catch ex As Exception
                    m_strSJKMC_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oDXLX属性
        '----------------------------------------------------------------
        Public Property oDXLX() As String
            Get
                oDXLX = m_strDXLX_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXLX_O = Value
                Catch ex As Exception
                    m_strDXLX_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oDXMC属性
        '----------------------------------------------------------------
        Public Property oDXMC() As String
            Get
                oDXMC = m_strDXMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXMC_O = Value
                Catch ex As Exception
                    m_strDXMC_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
