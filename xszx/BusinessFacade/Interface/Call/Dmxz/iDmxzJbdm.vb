Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IDmxzJbdm
    '
    ' 功能描述： 
    '     dmxz_zzry.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IDmxzJbdm
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '代码输入方式参数
        Public Enum enumCodeInputType
            ByDataGrid = 1        '由网格输入
            ByInput = 2           '由用户手工输入
        End Enum

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strTitle_I As String                    '模块标题
        Private m_strRowSourceSQL_I As String             '列表用的SQL字符串
        Private m_strInitField_I As String                '初始值对应的字段名
        Private m_strInitValue_I As String                '初始值
        Private m_strReturnCodeField_I As String          '返回的代码字段名
        Private m_strReturnNameField_I As String          '返回的名称字段名
        Private m_blnAllowInput_I As Boolean              '是否允许手工输入(默认True-允许)
        Private m_blnAllowNull_I As Boolean               '允许空输入(默认True-允许)
        Private m_blnMultiSelect_I As Boolean             '允许多选(默认True-允许)
        Private m_strColWidth_I As String                 '列宽说明(系统标准分隔符分隔)

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '返回方式：True-确定，False-取消
        Private m_strReturnCodeValue_O As String          '返回的代码字段对应的字段值
        Private m_strReturnNameValue_O As String          '返回的名称字段对应的字段值
        Private m_enumSelectMode_O As enumCodeInputType   '选择方式：1-网格，2-手工输入
        Private m_objDataSet_O As System.Data.DataSet     '多选时返回的数据集










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_strTitle_I = ""
            m_strRowSourceSQL_I = ""
            m_strInitField_I = ""
            m_strInitValue_I = ""
            m_strReturnCodeField_I = ""
            m_strReturnNameField_I = ""
            m_blnAllowInput_I = True
            m_blnAllowNull_I = True
            m_blnMultiSelect_I = True
            m_strColWidth_I = ""

            '初始化输出参数
            m_blnExitMode_O = False
            m_strReturnCodeValue_O = ""
            m_strReturnNameValue_O = ""
            m_enumSelectMode_O = enumCodeInputType.ByDataGrid
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
            ''释放资源
            'If Not (m_objDataSet_O Is Nothing) Then
            '    m_objDataSet_O.Dispose()
            '    m_objDataSet_O = Nothing
            'End If

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IDmxzJbdm)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' iTitle属性
        '----------------------------------------------------------------
        Public Property iTitle() As String
            Get
                iTitle = m_strTitle_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strTitle_I = Value
                Catch ex As Exception
                    m_strTitle_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iRowSourceSQL属性
        '----------------------------------------------------------------
        Public Property iRowSourceSQL() As String
            Get
                iRowSourceSQL = m_strRowSourceSQL_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRowSourceSQL_I = Value
                Catch ex As Exception
                    m_strRowSourceSQL_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iInitField属性
        '----------------------------------------------------------------
        Public Property iInitField() As String
            Get
                iInitField = m_strInitField_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInitField_I = Value
                Catch ex As Exception
                    m_strInitField_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iInitValue属性
        '----------------------------------------------------------------
        Public Property iInitValue() As String
            Get
                iInitValue = m_strInitValue_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInitValue_I = Value
                Catch ex As Exception
                    m_strInitValue_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCodeField属性
        '----------------------------------------------------------------
        Public Property iCodeField() As String
            Get
                iCodeField = m_strReturnCodeField_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnCodeField_I = Value
                Catch ex As Exception
                    m_strReturnCodeField_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iNameField属性
        '----------------------------------------------------------------
        Public Property iNameField() As String
            Get
                iNameField = m_strReturnNameField_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnNameField_I = Value
                Catch ex As Exception
                    m_strReturnNameField_I = ""
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
        ' iColWidth属性
        '----------------------------------------------------------------
        Public Property iColWidth() As String
            Get
                iColWidth = m_strColWidth_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strColWidth_I = Value
                Catch ex As Exception
                    m_strColWidth_I = ""
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
        ' oCodeValue属性
        '----------------------------------------------------------------
        Public Property oCodeValue() As String
            Get
                oCodeValue = m_strReturnCodeValue_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnCodeValue_O = Value
                Catch ex As Exception
                    m_strReturnCodeValue_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oNameValue属性
        '----------------------------------------------------------------
        Public Property oNameValue() As String
            Get
                oNameValue = m_strReturnNameValue_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnNameValue_O = Value
                Catch ex As Exception
                    m_strReturnNameValue_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oSelectMode属性
        '----------------------------------------------------------------
        Public Property oSelectMode() As enumCodeInputType
            Get
                oSelectMode = m_enumSelectMode_O
            End Get
            Set(ByVal Value As enumCodeInputType)
                Try
                    m_enumSelectMode_O = Value
                Catch ex As Exception
                    m_enumSelectMode_O = enumCodeInputType.ByDataGrid
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
