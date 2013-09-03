Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IGgxxGgzyZylmInfo
    '
    ' 功能描述： 
    '     ggxx_ggzy_zylm_info.aspx调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgxxGgzyZylmInfo
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '编辑模式
        Private m_intLMBS_I As Integer                                                       '查看、编辑、拷贝时用的栏目标识
        Private m_strLMDM_I As String                                                        '查看、编辑、拷贝时用的栏目代码
        Private m_strSJDM_I As String                                                        '增加、拷贝用的上级代码

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '返回方式：True-确定，False-取消
        Private m_intLMBS_O As Integer                    '返回正在处理的栏目标识
        Private m_strLMDM_O As String                     '返回正在处理的栏目代码
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
            m_intLMBS_I = 0
            m_strLMDM_I = ""
            m_strSJDM_I = ""

            '初始化输出参数
            m_blnExitMode_O = False
            m_intLMBS_O = 0
            m_strLMDM_O = ""
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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgxxGgzyZylmInfo)
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
        ' iLMBS属性
        '----------------------------------------------------------------
        Public Property iLMBS() As Integer
            Get
                iLMBS = m_intLMBS_I
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intLMBS_I = Value
                Catch ex As Exception
                    m_intLMBS_I = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iLMDM属性
        '----------------------------------------------------------------
        Public Property iLMDM() As String
            Get
                iLMDM = m_strLMDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strLMDM_I = Value
                Catch ex As Exception
                    m_strLMDM_I = ""
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
        ' oLMBS属性
        '----------------------------------------------------------------
        Public Property oLMBS() As Integer
            Get
                oLMBS = m_intLMBS_O
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intLMBS_O = Value
                Catch ex As Exception
                    m_intLMBS_O = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oLMDM属性
        '----------------------------------------------------------------
        Public Property oLMDM() As String
            Get
                oLMDM = m_strLMDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strLMDM_O = Value
                Catch ex As Exception
                    m_strLMDM_O = ""
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
