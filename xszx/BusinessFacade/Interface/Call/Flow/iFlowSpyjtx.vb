Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IFlowSpyjtx
    '
    ' 功能描述： 
    '     flow_spyjtx.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowSpyjtx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strFlowTypeName_I As String                        '工作流类型名称
        Private m_strWJBS_I As String                                '文件标识
        Private m_strSPR_I As String                                 '审批人
        Private m_strDLR_I As String                                 '补登人
        Private m_strInitYjlx_I As String                            '初始意见类型
        Private m_strPromptInfo_I As String                          '提示信息
        Private m_blnYjlxEnabled() As Boolean                        '能签批哪些意见
        Private m_blnDisplayXBBZ_I As Boolean                        '是否显示协办标志
        Private m_blnXBBZ_I As Boolean                               '是否为协办

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                           'True-确定,False-取消









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_strFlowTypeName_I = ""
            m_strWJBS_I = ""
            m_strSPR_I = ""
            m_strDLR_I = ""
            m_strInitYjlx_I = ""
            m_strPromptInfo_I = ""
            m_blnYjlxEnabled = Nothing
            m_blnDisplayXBBZ_I = False
            m_blnXBBZ_I = False

            '初始化输出参数
            m_blnExitMode_O = False

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
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowSpyjtx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' iFlowTypeName属性
        '----------------------------------------------------------------
        Public Property iFlowTypeName() As String
            Get
                iFlowTypeName = m_strFlowTypeName_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFlowTypeName_I = Value
                Catch ex As Exception
                    m_strFlowTypeName_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJBS属性
        '----------------------------------------------------------------
        Public Property iWJBS() As String
            Get
                iWJBS = m_strWJBS_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJBS_I = Value
                Catch ex As Exception
                    m_strWJBS_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSPR属性
        '----------------------------------------------------------------
        Public Property iSPR() As String
            Get
                iSPR = m_strSPR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSPR_I = Value
                Catch ex As Exception
                    m_strSPR_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDLR属性
        '----------------------------------------------------------------
        Public Property iDLR() As String
            Get
                iDLR = m_strDLR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDLR_I = Value
                Catch ex As Exception
                    m_strDLR_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iInitYjlx属性
        '----------------------------------------------------------------
        Public Property iInitYjlx() As String
            Get
                iInitYjlx = m_strInitYjlx_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInitYjlx_I = Value
                Catch ex As Exception
                    m_strInitYjlx_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iPromptInfo属性
        '----------------------------------------------------------------
        Public Property iPromptInfo() As String
            Get
                iPromptInfo = m_strPromptInfo_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strPromptInfo_I = Value
                Catch ex As Exception
                    m_strPromptInfo_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iYjlxEnabled属性
        '----------------------------------------------------------------
        Public Property iYjlxEnabled() As Boolean()
            Get
                iYjlxEnabled = m_blnYjlxEnabled
            End Get
            Set(ByVal Value As Boolean())
                Try
                    m_blnYjlxEnabled = Value
                Catch ex As Exception
                    m_blnYjlxEnabled = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDisplayXBBZ属性
        '----------------------------------------------------------------
        Public Property iDisplayXBBZ() As Boolean
            Get
                iDisplayXBBZ = m_blnDisplayXBBZ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnDisplayXBBZ_I = Value
                Catch ex As Exception
                    m_blnDisplayXBBZ_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iXBBZ属性
        '----------------------------------------------------------------
        Public Property iXBBZ() As Boolean
            Get
                iXBBZ = m_blnXBBZ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnXBBZ_I = Value
                Catch ex As Exception
                    m_blnXBBZ_I = False
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

    End Class

End Namespace
