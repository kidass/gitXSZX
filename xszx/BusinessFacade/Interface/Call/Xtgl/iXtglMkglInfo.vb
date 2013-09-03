Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IXtglMkglInfo
    '
    ' 功能描述： 
    '     xtgl_mkgl_info.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IXtglMkglInfo
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '编辑模式
        Private m_intMKBS_I As Integer                    '查看、编辑、拷贝时用的模块标识
        Private m_strMKDM_I As String                     '查看、编辑、拷贝时用的模块代码
        Private m_strSJDM_I As String                     '增加、拷贝用的上级代码

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '返回方式：True-确定，False-取消
        Private m_intMKBS_O As Integer                    '返回正在处理的模块标识
        Private m_strMKDM_O As String                     '返回正在处理的模块代码
        Private m_strSJDM_O As String                     '返回正在处理的上级代码










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
            m_intMKBS_I = 0
            m_strMKDM_I = ""
            m_strSJDM_I = ""

            '初始化输出参数
            m_blnExitMode_O = False
            m_intMKBS_O = 0
            m_strMKDM_O = ""
            m_strSJDM_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IXtglMkglInfo)
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
        ' iMKBS属性
        '----------------------------------------------------------------
        Public Property iMKBS() As Integer
            Get
                iMKBS = m_intMKBS_I
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intMKBS_I = Value
                Catch ex As Exception
                    m_intMKBS_I = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iMKDM属性
        '----------------------------------------------------------------
        Public Property iMKDM() As String
            Get
                iMKDM = m_strMKDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strMKDM_I = Value
                Catch ex As Exception
                    m_strMKDM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSJDM属性
        '----------------------------------------------------------------
        Public Property iSJDM() As String
            Get
                iSJDM = m_strSJDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJDM_I = Value
                Catch ex As Exception
                    m_strSJDM_I = ""
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
        ' oMKBS属性
        '----------------------------------------------------------------
        Public Property oMKBS() As Integer
            Get
                oMKBS = m_intMKBS_O
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intMKBS_O = Value
                Catch ex As Exception
                    m_intMKBS_O = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oMKDM属性
        '----------------------------------------------------------------
        Public Property oMKDM() As String
            Get
                oMKDM = m_strMKDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strMKDM_O = Value
                Catch ex As Exception
                    m_strMKDM_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oSJDM属性
        '----------------------------------------------------------------
        Public Property oSJDM() As String
            Get
                oSJDM = m_strSJDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJDM_O = Value
                Catch ex As Exception
                    m_strSJDM_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
