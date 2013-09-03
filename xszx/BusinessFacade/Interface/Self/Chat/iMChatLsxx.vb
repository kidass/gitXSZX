Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMChatLsxx
    '
    ' 功能描述： 
    '     chat_lsxx.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMChatLsxx
        Implements IDisposable

        '----------------------------------------------------------------
        'textbox
        '----------------------------------------------------------------
        Private m_strtxtJSXXPageIndex As String                         'txtJSXXPageIndex
        Private m_strtxtJSXXPageSize As String                          'txtJSXXPageSize

        Private m_strtxtJSXXSearch_FSR As String                        'txtJSXXSearch_FSR
        Private m_strtxtJSXXSearch_JSR As String                        'txtJSXXSearch_JSR
        Private m_strtxtJSXXSearch_XX As String                         'txtJSXXSearch_XX
        Private m_strtxtJSXXSearch_FSSJMin As String                    'txtJSXXSearch_FSSJMin
        Private m_strtxtJSXXSearch_FSSJMax As String                    'txtJSXXSearch_FSSJMax
        Private m_strtxtJSXXSearch_FJNR As String                       'txtJSXXSearch_FJNR

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdJSXXQuery As String                   'htxtSessionIdJSXXQuery
        Private m_strhtxtJSXXQuery As String                            'htxtJSXXQuery
        Private m_strhtxtJSXXRows As String                             'htxtJSXXRows
        Private m_strhtxtJSXXSort As String                             'htxtJSXXSort
        Private m_strhtxtJSXXSortColumnIndex As String                  'htxtJSXXSortColumnIndex
        Private m_strhtxtJSXXSortType As String                         'htxtJSXXSortType
        Private m_strhtxtDivLeftJSXX As String                          'htxtDivLeftJSXX
        Private m_strhtxtDivTopJSXX As String                           'htxtDivTopJSXX
        Private m_strhtxtDivLeftBody As String                          'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                           'htxtDivTopBody

        '----------------------------------------------------------------
        'grdJSXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_grdJSXX As Integer                        'grdJSXX的页大小
        Private m_intSelectedIndex_grdJSXX As Integer                   'grdJSXX的行索引
        Private m_intCurrentPageIndex_grdJSXX As Integer                'grdJSXX的页索引









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strtxtJSXXPageIndex = ""
            m_strtxtJSXXPageSize = ""

            m_strtxtJSXXSearch_FSR = ""
            m_strtxtJSXXSearch_JSR = ""
            m_strtxtJSXXSearch_XX = ""
            m_strtxtJSXXSearch_FSSJMin = ""
            m_strtxtJSXXSearch_FSSJMax = ""
            m_strtxtJSXXSearch_FJNR = ""

            m_strhtxtSessionIdJSXXQuery = ""
            m_strhtxtJSXXQuery = ""
            m_strhtxtJSXXRows = ""
            m_strhtxtJSXXSort = ""
            m_strhtxtJSXXSortColumnIndex = ""
            m_strhtxtJSXXSortType = ""

            m_strhtxtDivLeftJSXX = ""
            m_strhtxtDivTopJSXX = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_intPageSize_grdJSXX = 100
            m_intSelectedIndex_grdJSXX = -1
            m_intCurrentPageIndex_grdJSXX = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMChatLsxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' txtJSXXPageIndex属性
        '----------------------------------------------------------------
        Public Property txtJSXXPageIndex() As String
            Get
                txtJSXXPageIndex = m_strtxtJSXXPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSXXPageIndex = Value
                Catch ex As Exception
                    m_strtxtJSXXPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSXXPageSize属性
        '----------------------------------------------------------------
        Public Property txtJSXXPageSize() As String
            Get
                txtJSXXPageSize = m_strtxtJSXXPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSXXPageSize = Value
                Catch ex As Exception
                    m_strtxtJSXXPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtJSXXSearch_FSR属性
        '----------------------------------------------------------------
        Public Property txtJSXXSearch_FSR() As String
            Get
                txtJSXXSearch_FSR = m_strtxtJSXXSearch_FSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSXXSearch_FSR = Value
                Catch ex As Exception
                    m_strtxtJSXXSearch_FSR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSXXSearch_JSR属性
        '----------------------------------------------------------------
        Public Property txtJSXXSearch_JSR() As String
            Get
                txtJSXXSearch_JSR = m_strtxtJSXXSearch_JSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSXXSearch_JSR = Value
                Catch ex As Exception
                    m_strtxtJSXXSearch_JSR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSXXSearch_XX属性
        '----------------------------------------------------------------
        Public Property txtJSXXSearch_XX() As String
            Get
                txtJSXXSearch_XX = m_strtxtJSXXSearch_XX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSXXSearch_XX = Value
                Catch ex As Exception
                    m_strtxtJSXXSearch_XX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSXXSearch_FSSJMin属性
        '----------------------------------------------------------------
        Public Property txtJSXXSearch_FSSJMin() As String
            Get
                txtJSXXSearch_FSSJMin = m_strtxtJSXXSearch_FSSJMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSXXSearch_FSSJMin = Value
                Catch ex As Exception
                    m_strtxtJSXXSearch_FSSJMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSXXSearch_FSSJMax属性
        '----------------------------------------------------------------
        Public Property txtJSXXSearch_FSSJMax() As String
            Get
                txtJSXXSearch_FSSJMax = m_strtxtJSXXSearch_FSSJMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSXXSearch_FSSJMax = Value
                Catch ex As Exception
                    m_strtxtJSXXSearch_FSSJMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSXXSearch_FJNR属性
        '----------------------------------------------------------------
        Public Property txtJSXXSearch_FJNR() As String
            Get
                txtJSXXSearch_FJNR = m_strtxtJSXXSearch_FJNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSXXSearch_FJNR = Value
                Catch ex As Exception
                    m_strtxtJSXXSearch_FJNR = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtSessionIdJSXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdJSXXQuery() As String
            Get
                htxtSessionIdJSXXQuery = m_strhtxtSessionIdJSXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdJSXXQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdJSXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSXXSort属性
        '----------------------------------------------------------------
        Public Property htxtJSXXSort() As String
            Get
                htxtJSXXSort = m_strhtxtJSXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSXXSort = Value
                Catch ex As Exception
                    m_strhtxtJSXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSXXRows属性
        '----------------------------------------------------------------
        Public Property htxtJSXXRows() As String
            Get
                htxtJSXXRows = m_strhtxtJSXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSXXRows = Value
                Catch ex As Exception
                    m_strhtxtJSXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtJSXXSortColumnIndex() As String
            Get
                htxtJSXXSortColumnIndex = m_strhtxtJSXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtJSXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtJSXXQuery() As String
            Get
                htxtJSXXQuery = m_strhtxtJSXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSXXQuery = Value
                Catch ex As Exception
                    m_strhtxtJSXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtJSXXSortType() As String
            Get
                htxtJSXXSortType = m_strhtxtJSXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSXXSortType = Value
                Catch ex As Exception
                    m_strhtxtJSXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftJSXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftJSXX() As String
            Get
                htxtDivLeftJSXX = m_strhtxtDivLeftJSXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftJSXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftJSXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopJSXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopJSXX() As String
            Get
                htxtDivTopJSXX = m_strhtxtDivTopJSXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopJSXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopJSXX = ""
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
        ' grdJSXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdJSXX_PageSize() As Integer
            Get
                grdJSXX_PageSize = m_intPageSize_grdJSXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdJSXX = Value
                Catch ex As Exception
                    m_intPageSize_grdJSXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJSXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdJSXX_SelectedIndex() As Integer
            Get
                grdJSXX_SelectedIndex = m_intSelectedIndex_grdJSXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdJSXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdJSXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJSXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdJSXX_CurrentPageIndex() As Integer
            Get
                grdJSXX_CurrentPageIndex = m_intCurrentPageIndex_grdJSXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdJSXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdJSXX = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
