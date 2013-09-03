Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IChatLsxx
    '
    ' 功能描述： 
    '     chat_lsxx.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IChatLsxx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_blnDifferentFrame_I As Boolean       '调用帧与当前帧不同（跨帧调用）
        Private m_strUserXM_I As String                '要处理的用户名称

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean             '退出方式：True-确定，False-取消









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_blnDifferentFrame_I = False
            m_strUserXM_I = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IChatLsxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iDifferentFrame属性
        '----------------------------------------------------------------
        Public Property iDifferentFrame() As Boolean
            Get
                iDifferentFrame = m_blnDifferentFrame_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnDifferentFrame_I = Value
                Catch ex As Exception
                    m_blnDifferentFrame_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iUserXM属性
        '----------------------------------------------------------------
        Public Property iUserXM() As String
            Get
                iUserXM = m_strUserXM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strUserXM_I = Value
                Catch ex As Exception
                    m_strUserXM_I = ""
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
