Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowShouhui
    '
    ' 功能描述： 
    '     flow_shouhui.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowShouhui
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtJSRXXQuery As String                            'htxtJSRXXQuery
        Private m_strhtxtJSRXXRows As String                             'htxtJSRXXRows
        Private m_strhtxtJSRXXSort As String                             'htxtJSRXXSort
        Private m_strhtxtJSRXXSortColumnIndex As String                  'htxtJSRXXSortColumnIndex
        Private m_strhtxtJSRXXSortType As String                         'htxtJSRXXSortType
        Private m_strhtxtDivLeftJSRXX As String                          'htxtDivLeftJSRXX
        Private m_strhtxtDivTopJSRXX As String                           'htxtDivTopJSRXX
        Private m_strhtxtDivLeftBody As String                           'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                            'htxtDivTopBody

        '----------------------------------------------------------------
        'checkbox
        '----------------------------------------------------------------
        Private m_blnSelected_chkSHTZ As Boolean                         'chkSHTZ

        '----------------------------------------------------------------
        'grdJSRXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_JSRXX As Integer                           'grdJSRXX的页大小
        Private m_intSelectedIndex_JSRXX As Integer                      'grdJSRXX的行索引
        Private m_intCurrentPageIndex_JSRXX As Integer                   'grdJSRXX的页索引












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtJSRXXQuery = ""
            m_strhtxtJSRXXRows = ""
            m_strhtxtJSRXXSort = ""
            m_strhtxtJSRXXSortColumnIndex = ""
            m_strhtxtJSRXXSortType = ""

            m_strhtxtDivLeftJSRXX = ""
            m_strhtxtDivTopJSRXX = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_blnSelected_chkSHTZ = False

            m_intPageSize_JSRXX = 100
            m_intSelectedIndex_JSRXX = -1
            m_intCurrentPageIndex_JSRXX = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowShouhui)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' htxtJSRXXSort属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXSort() As String
            Get
                htxtJSRXXSort = m_strhtxtJSRXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXSort = Value
                Catch ex As Exception
                    m_strhtxtJSRXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSRXXRows属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXRows() As String
            Get
                htxtJSRXXRows = m_strhtxtJSRXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXRows = Value
                Catch ex As Exception
                    m_strhtxtJSRXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSRXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXSortColumnIndex() As String
            Get
                htxtJSRXXSortColumnIndex = m_strhtxtJSRXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtJSRXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSRXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXQuery() As String
            Get
                htxtJSRXXQuery = m_strhtxtJSRXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXQuery = Value
                Catch ex As Exception
                    m_strhtxtJSRXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSRXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXSortType() As String
            Get
                htxtJSRXXSortType = m_strhtxtJSRXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXSortType = Value
                Catch ex As Exception
                    m_strhtxtJSRXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftJSRXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftJSRXX() As String
            Get
                htxtDivLeftJSRXX = m_strhtxtDivLeftJSRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftJSRXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftJSRXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopJSRXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopJSRXX() As String
            Get
                htxtDivTopJSRXX = m_strhtxtDivTopJSRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopJSRXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopJSRXX = ""
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
        ' chkSHTZ_Selected属性
        '----------------------------------------------------------------
        Public Property chkSHTZ_Selected() As Boolean
            Get
                chkSHTZ_Selected = m_blnSelected_chkSHTZ
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSelected_chkSHTZ = Value
                Catch ex As Exception
                    m_blnSelected_chkSHTZ = False
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdJSRXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdJSRXX_PageSize() As Integer
            Get
                grdJSRXX_PageSize = m_intPageSize_JSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_JSRXX = Value
                Catch ex As Exception
                    m_intPageSize_JSRXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJSRXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdJSRXX_SelectedIndex() As Integer
            Get
                grdJSRXX_SelectedIndex = m_intSelectedIndex_JSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_JSRXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_JSRXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJSRXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdJSRXX_CurrentPageIndex() As Integer
            Get
                grdJSRXX_CurrentPageIndex = m_intCurrentPageIndex_JSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_JSRXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_JSRXX = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
