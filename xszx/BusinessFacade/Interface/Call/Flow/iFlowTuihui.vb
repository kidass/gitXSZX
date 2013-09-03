Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IFlowTuihui
    '
    ' 功能描述： 
    '     flow_tuihui.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowTuihui
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strFlowTypeName_I As String                        '工作流类型名称
        Private m_strWJBS_I As String                                '文件标识
        Private m_blnCanReadFile_I As Boolean                        '是否保留阅读文件权利

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
            m_blnCanReadFile_I = True
            m_strWJBS_I = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowTuihui)
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
        ' iCanReadFile属性
        '----------------------------------------------------------------
        Public Property iCanReadFile() As Boolean
            Get
                iCanReadFile = m_blnCanReadFile_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanReadFile_I = Value
                Catch ex As Exception
                    m_blnCanReadFile_I = False
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
