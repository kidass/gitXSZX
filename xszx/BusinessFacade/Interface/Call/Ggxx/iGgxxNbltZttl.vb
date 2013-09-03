Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IGgxxNbltZttl
    '
    ' 功能描述： 
    '     ggxx_nblt_zttl.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgxxNbltZttl
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strJLBH_I As String                      '交流编号

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                 '返回方式：True-确定，False-取消
        Private m_strJLBH_O As String                      '返回正在处理的交流编号









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_strJLBH_I = ""

            '初始化输出参数
            m_blnExitMode_O = False
            m_strJLBH_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgxxNbltZttl)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' iJLBH属性
        '----------------------------------------------------------------
        Public Property iJLBH() As String
            Get
                iJLBH = m_strJLBH_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strJLBH_I = Value
                Catch ex As Exception
                    m_strJLBH_I = ""
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
        ' oJLBH属性
        '----------------------------------------------------------------
        Public Property oJLBH() As String
            Get
                oJLBH = m_strJLBH_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strJLBH_O = Value
                Catch ex As Exception
                    m_strJLBH_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
