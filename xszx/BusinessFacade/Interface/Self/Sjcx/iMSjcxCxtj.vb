Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMSjcxCxtj
    '
    ' 功能描述： 
    '     sjcx_cxtj.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMSjcxCxtj
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strhtxtDivLeftTJ As String              'htxtDivLeftTJ
        Private m_strhtxtDivTopTJ As String               'htxtDivTopTJ
        Private m_strhtxtDivLeftBody As String            'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String             'htxtDivTopBody

        Private m_strhtxtSessionIDTJ As String            'htxtSessionIDTJ
        Private m_strhtxtTJSort As String                 'htxtTJSort
        Private m_strhtxtTJSortColumnIndex As String      'htxtTJSortColumnIndex
        Private m_strhtxtTJSortType As String             'htxtTJSortType

        Private m_strtxtZKHZ As String                    'txtZKHZ
        Private m_strtxtVal1 As String                    'txtVal1
        Private m_strtxtVal2 As String                    'txtVal2
        Private m_strtxtYKHZ As String                    'txtYKHZ

        Private m_intSelectedIndex_rblBJF As Integer      'SelectedIndex_rblBJF
        Private m_intSelectedIndex_rblLJF As Integer      'SelectedIndex_rblLJF
        Private m_intSelectedIndex_lstField As Integer    'SelectedIndex of lstField

        Private m_intCurrentPageIndex_grdTJ As Integer    'CurrentPageIndex of grdTJ
        Private m_intSelectedIndex_grdTJ As Integer       'SelectedIndex of grdTJ
        Private m_intPageSize_grdTJ As Integer            'PageSize of grdTJ










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strhtxtDivLeftTJ = ""
            m_strhtxtDivTopTJ = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIDTJ = ""
            m_strhtxtTJSort = ""
            m_strhtxtTJSortColumnIndex = ""
            m_strhtxtTJSortType = ""

            m_strtxtZKHZ = ""
            m_strtxtVal1 = ""
            m_strtxtVal2 = ""
            m_strtxtYKHZ = ""

            m_intSelectedIndex_rblBJF = 0
            m_intSelectedIndex_rblLJF = 0
            m_intSelectedIndex_lstField = 0

            m_intCurrentPageIndex_grdTJ = 0
            m_intSelectedIndex_grdTJ = -1
            m_intPageSize_grdTJ = 100

        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMSjcxCxtj)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtSessionIDTJ属性
        '----------------------------------------------------------------
        Public Property htxtSessionIDTJ() As String
            Get
                htxtSessionIDTJ = m_strhtxtSessionIDTJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIDTJ = Value
                Catch ex As Exception
                    m_strhtxtSessionIDTJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTJSort属性
        '----------------------------------------------------------------
        Public Property htxtTJSort() As String
            Get
                htxtTJSort = m_strhtxtTJSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTJSort = Value
                Catch ex As Exception
                    m_strhtxtTJSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTJSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtTJSortColumnIndex() As String
            Get
                htxtTJSortColumnIndex = m_strhtxtTJSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTJSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtTJSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTJSortType属性
        '----------------------------------------------------------------
        Public Property htxtTJSortType() As String
            Get
                htxtTJSortType = m_strhtxtTJSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTJSortType = Value
                Catch ex As Exception
                    m_strhtxtTJSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtZKHZ属性
        '----------------------------------------------------------------
        Public Property txtZKHZ() As String
            Get
                txtZKHZ = m_strtxtZKHZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZKHZ = Value
                Catch ex As Exception
                    m_strtxtZKHZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtVal1属性
        '----------------------------------------------------------------
        Public Property txtVal1() As String
            Get
                txtVal1 = m_strtxtVal1
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtVal1 = Value
                Catch ex As Exception
                    m_strtxtVal1 = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtVal2属性
        '----------------------------------------------------------------
        Public Property txtVal2() As String
            Get
                txtVal2 = m_strtxtVal2
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtVal2 = Value
                Catch ex As Exception
                    m_strtxtVal2 = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYKHZ属性
        '----------------------------------------------------------------
        Public Property txtYKHZ() As String
            Get
                txtYKHZ = m_strtxtYKHZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYKHZ = Value
                Catch ex As Exception
                    m_strtxtYKHZ = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' rblBJF_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property rblBJF_SelectedIndex() As Integer
            Get
                rblBJF_SelectedIndex = m_intSelectedIndex_rblBJF
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblBJF = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblBJF = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' rblLJF_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property rblLJF_SelectedIndex() As Integer
            Get
                rblLJF_SelectedIndex = m_intSelectedIndex_rblLJF
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblLJF = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblLJF = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' lstField_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property lstField_SelectedIndex() As Integer
            Get
                lstField_SelectedIndex = m_intSelectedIndex_lstField
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_lstField = Value
                Catch ex As Exception
                    m_intSelectedIndex_lstField = 0
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftBody属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftBody() As String
            Get
                htxtDivLeftBody = m_strhtxtDivLeftBody
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftBody = Value
                Catch ex As Exception
                    m_strhtxtDivLeftBody = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopBody属性
        '----------------------------------------------------------------
        Public Property htxtDivTopBody() As String
            Get
                htxtDivTopBody = m_strhtxtDivTopBody
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopBody = Value
                Catch ex As Exception
                    m_strhtxtDivTopBody = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftTJ属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftTJ() As String
            Get
                htxtDivLeftTJ = m_strhtxtDivLeftTJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftTJ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftTJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopTJ属性
        '----------------------------------------------------------------
        Public Property htxtDivTopTJ() As String
            Get
                htxtDivTopTJ = m_strhtxtDivTopTJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopTJ = Value
                Catch ex As Exception
                    m_strhtxtDivTopTJ = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdTJ_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdTJ_CurrentPageIndex() As Integer
            Get
                grdTJ_CurrentPageIndex = m_intCurrentPageIndex_grdTJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdTJ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdTJ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdTJ_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdTJ_SelectedIndex() As Integer
            Get
                grdTJ_SelectedIndex = m_intSelectedIndex_grdTJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdTJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdTJ = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdTJ_PageSize属性
        '----------------------------------------------------------------
        Public Property grdTJ_PageSize() As Integer
            Get
                grdTJ_PageSize = m_intPageSize_grdTJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdTJ = Value
                Catch ex As Exception
                    m_intPageSize_grdTJ = 100
                End Try
            End Set
        End Property

    End Class

End Namespace
