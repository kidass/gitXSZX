Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IFlowXgwjljAdd
    '
    ' 功能描述： 
    '     flow_xgwjlj_add.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowXgwjljAdd
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_objDataSet_XGWJ_I As Xydc.Platform.Common.Data.FlowData '相关文件数据
        Private m_strFlowTypeName_I As String                          '工作流类型名称
        Private m_strWJBS_I As String                                  '文件标识

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        '输出变动后的附件数据 = m_objDataSet_XGWJ_I









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_strFlowTypeName_I = ""
            m_objDataSet_XGWJ_I = Nothing
            m_strWJBS_I = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowXgwjljAdd)
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
        ' iDataSet_XGWJ属性
        '----------------------------------------------------------------
        Public Property iDataSet_XGWJ() As Xydc.Platform.Common.Data.FlowData
            Get
                iDataSet_XGWJ = m_objDataSet_XGWJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_XGWJ_I = Value
                Catch ex As Exception
                    m_objDataSet_XGWJ_I = Nothing
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' oDataSet_XGWJ属性
        '----------------------------------------------------------------
        Public Property oDataSet_XGWJ() As Xydc.Platform.Common.Data.FlowData
            Get
                oDataSet_XGWJ = m_objDataSet_XGWJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_XGWJ_I = Value
                Catch ex As Exception
                    m_objDataSet_XGWJ_I = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
