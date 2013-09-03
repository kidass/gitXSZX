Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IGgdmBmryRyxx
    '
    ' 功能描述： 
    '     ggdm_bmry_ryxx.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgdmBmryRyxx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '编辑模式
        Private m_strRYDM_I As String                     '查看、编辑、拷贝时用的人员代码
        Private m_strZZDM_I As String                     '增加、拷贝时的组织代码

        Private m_intExitMode_I As Integer                '1-人员信息，2-修改密码，3-角色，4-范围


        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '返回方式：True-确定，False-取消
        Private m_strRYDM_O As String                     '返回正在处理的人员代码
        Private m_strRYMC_O As String                     '返回正在处理的人员名称

        Private m_intExitMode_0 As Integer                '1-人员信息，2-修改密码，3-角色，4-范围










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect

            m_intExitMode_I = 0

            m_strRYDM_I = ""
            m_strZZDM_I = ""

            '初始化输出参数
            m_blnExitMode_O = False

            m_intExitMode_0 = 0

            m_strRYDM_O = ""
            m_strRYMC_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgdmBmryRyxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub







        '----------------------------------------------------------------
        ' iIntEditMode属性

        '----------------------------------------------------------------
        Public Property iIntEditMode() As Integer
            Get
                iIntEditMode = m_intExitMode_I
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intExitMode_I = Value
                Catch ex As Exception
                    m_intExitMode_I = 0
                End Try
            End Set
        End Property


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
        ' iRYDM属性
        '----------------------------------------------------------------
        Public Property iRYDM() As String
            Get
                iRYDM = m_strRYDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRYDM_I = Value
                Catch ex As Exception
                    m_strRYDM_I = ""
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
        ' oIntExitMode属性

        '----------------------------------------------------------------
        Public Property oIntExitMode() As Integer
            Get
                oIntExitMode = m_intExitMode_0
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intExitMode_0 = Value
                Catch ex As Exception
                    m_intExitMode_0 = 0
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
        ' oRYDM属性
        '----------------------------------------------------------------
        Public Property oRYDM() As String
            Get
                oRYDM = m_strRYDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRYDM_O = Value
                Catch ex As Exception
                    m_strRYDM_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oRYMC属性
        '----------------------------------------------------------------
        Public Property oRYMC() As String
            Get
                oRYMC = m_strRYMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRYMC_O = Value
                Catch ex As Exception
                    m_strRYMC_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
