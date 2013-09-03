Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：ISjcxCxtj
    '
    ' 功能描述： 
    '     sjcx_cxtj.aspx模块调用接口的定义与处理
    '
    ' 备注信息：
    '     m_objQueryTable_I中默认表前缀为“a.”
    '----------------------------------------------------------------
    <Serializable()> Public Class ISjcxCxtj
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_objQueryTable_I As System.Data.DataTable               '要检索的表对象
        Private m_objDataSetTJ_I As Xydc.Platform.Common.Data.QueryData     '现有查询条件
        Private m_strFixQuery_I As String                                '必须设置的查询条件
        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                               '返回方式：True-确定，False-取消
        Private m_objDataSetTJ_O As Xydc.Platform.Common.Data.QueryData     '返回查询条件
        Private m_strQuery_O As String                                   '返回查询条件字符串












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_objQueryTable_I = Nothing
            'm_objDataSetTJ_I = Nothing
            m_strFixQuery_I = ""

            '初始化输出参数
            m_blnExitMode_O = False
            'm_objDataSetTJ_O = Nothing
            m_strQuery_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.ISjcxCxtj)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' iQueryTable属性
        '----------------------------------------------------------------
        Public Property iQueryTable() As System.Data.DataTable
            Get
                iQueryTable = m_objQueryTable_I
            End Get
            Set(ByVal Value As System.Data.DataTable)
                Try
                    m_objQueryTable_I = Value
                Catch ex As Exception
                    m_objQueryTable_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        'iDataSetTJ属性
        '----------------------------------------------------------------
        Public Property iDataSetTJ() As Xydc.Platform.Common.Data.QueryData
            Get
                iDataSetTJ = m_objDataSetTJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.QueryData)
                Try
                    m_objDataSetTJ_I = Value
                Catch ex As Exception
                    m_objDataSetTJ_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iFixQuery属性
        '----------------------------------------------------------------
        Public Property iFixQuery() As String
            Get
                iFixQuery = m_strFixQuery_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFixQuery_I = Value
                Catch ex As Exception
                    m_strFixQuery_I = ""
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
        'oDataSetTJ属性
        '----------------------------------------------------------------
        Public Property oDataSetTJ() As Xydc.Platform.Common.Data.QueryData
            Get
                oDataSetTJ = m_objDataSetTJ_O
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.QueryData)
                Try
                    m_objDataSetTJ_O = Value
                Catch ex As Exception
                    m_objDataSetTJ_O = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oQueryString属性
        '----------------------------------------------------------------
        Public Property oQueryString() As String
            Get
                oQueryString = m_strQuery_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strQuery_O = Value
                Catch ex As Exception
                    m_strQuery_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
