Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IGgdmBmryBmxx
    '
    ' 功能描述： 
    '     ggdm_bmry_bmxx.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgdmBmryBmxx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '编辑模式
        Private m_strPrevZZDM_I As String                 '增加、拷贝时的上级代码
        Private m_strZZDM_I As String                     '查看、编辑、拷贝时用的组织代码

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '返回方式：True-确定，False-取消
        Private m_strZZDM_O As String                     '返回正在处理的组织代码
        Private m_strZZMC_O As String                     '返回正在处理的组织名称









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
            m_strZZDM_I = ""
            m_strPrevZZDM_I = ""

            '初始化输出参数
            m_blnExitMode_O = False
            m_strZZDM_O = ""
            m_strZZMC_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgdmBmryBmxx)
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
        ' iZZDM属性
        '----------------------------------------------------------------
        Public Property iZZDM() As String
            Get
                iZZDM = m_strZZDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZZDM_I = Value
                Catch ex As Exception
                    m_strZZDM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iPrevZZDM属性
        '----------------------------------------------------------------
        Public Property iPrevZZDM() As String
            Get
                iPrevZZDM = m_strPrevZZDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strPrevZZDM_I = Value
                Catch ex As Exception
                    m_strPrevZZDM_I = ""
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
        ' oZZDM属性
        '----------------------------------------------------------------
        Public Property oZZDM() As String
            Get
                oZZDM = m_strZZDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZZDM_O = Value
                Catch ex As Exception
                    m_strZZDM_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oZZMC属性
        '----------------------------------------------------------------
        Public Property oZZMC() As String
            Get
                oZZMC = m_strZZMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZZMC_O = Value
                Catch ex As Exception
                    m_strZZMC_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
