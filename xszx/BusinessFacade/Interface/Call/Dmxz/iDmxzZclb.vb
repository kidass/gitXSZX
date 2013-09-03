Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IDmxzZclb
    '
    ' 功能描述： 
    '     dmxz_zclb.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IDmxzZclb
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strInputList_I As String            '现有类别列表
        Private m_blnMultiSelect_I As Boolean         '可以多重选择?false-不能,true-能(缺省)
        Private m_blnSelectFFFW_I As Boolean          '可以选择范围?false-不能,true-能(缺省)
        Private m_blnAllowInput_I As Boolean          '可以手工输入?false-不能,true-能(缺省)
        Private m_blnAllowNull_I As Boolean           '允许空输入?false-不能,true-能(缺省)

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean            '退出方式：True-确定，False-取消
        Private m_strOutputList_O As String           '新选择的类别列表,按系统指定的分隔符分隔(CharSeparate)
        Private m_objDataSet_O As System.Data.DataSet '多选时返回的数据集









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_strInputList_I = ""
            m_blnMultiSelect_I = True
            m_blnSelectFFFW_I = True
            m_blnAllowInput_I = True
            m_blnAllowNull_I = True

            '初始化输出参数
            m_blnExitMode_O = False
            m_strOutputList_O = ""
            m_objDataSet_O = Nothing

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
            Try
                If Not (m_objDataSet_O Is Nothing) Then
                    m_objDataSet_O.Dispose()
                    m_objDataSet_O = Nothing
                End If
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IDmxzZclb)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iLeibieList属性
        '----------------------------------------------------------------
        Public Property iLeibieList() As String
            Get
                iLeibieList = m_strInputList_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInputList_I = Value
                Catch ex As Exception
                    m_strInputList_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iMultiSelect属性
        '----------------------------------------------------------------
        Public Property iMultiSelect() As Boolean
            Get
                iMultiSelect = m_blnMultiSelect_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnMultiSelect_I = Value
                Catch ex As Exception
                    m_blnMultiSelect_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSelectFFFW属性
        '----------------------------------------------------------------
        Public Property iSelectFFFW() As Boolean
            Get
                iSelectFFFW = m_blnSelectFFFW_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSelectFFFW_I = Value
                Catch ex As Exception
                    m_blnSelectFFFW_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iAllowInput属性
        '----------------------------------------------------------------
        Public Property iAllowInput() As Boolean
            Get
                iAllowInput = m_blnAllowInput_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAllowInput_I = Value
                Catch ex As Exception
                    m_blnAllowInput_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iAllowNull属性
        '----------------------------------------------------------------
        Public Property iAllowNull() As Boolean
            Get
                iAllowNull = m_blnAllowNull_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAllowNull_I = Value
                Catch ex As Exception
                    m_blnAllowNull_I = False
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
        ' oLeibieList属性
        '----------------------------------------------------------------
        Public Property oLeibieList() As String
            Get
                oLeibieList = m_strOutputList_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strOutputList_O = Value
                Catch ex As Exception
                    m_strOutputList_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oDataSet属性
        '----------------------------------------------------------------
        Public Property oDataSet() As System.Data.DataSet
            Get
                oDataSet = m_objDataSet_O
            End Get
            Set(ByVal Value As System.Data.DataSet)
                Try
                    m_objDataSet_O = Value
                Catch ex As Exception
                    m_objDataSet_O = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
