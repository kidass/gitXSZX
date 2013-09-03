Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IGrswCyyj
    '
    ' 功能描述： 
    '     grsw_cyyj.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IGrswCyyj
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strBLR_I As String                                 '当前办理人

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                           'True-确定,False-取消
        Private m_strOpinion_O As String                             '返回选择意见









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_strBLR_I = ""
            m_strOpinion_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGrswCyyj)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iBLR属性
        '----------------------------------------------------------------
        Public Property iBLR() As String
            Get
                iBLR = m_strBLR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strBLR_I = Value
                Catch ex As Exception
                    m_strBLR_I = ""
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
        ' oOpinion属性
        '----------------------------------------------------------------
        Public Property oOpinion() As String
            Get
                oOpinion = m_strOpinion_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strOpinion_O = Value
                Catch ex As Exception
                    m_strOpinion_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
