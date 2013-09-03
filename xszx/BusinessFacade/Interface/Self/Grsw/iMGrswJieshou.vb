Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGrswJieshou
    '
    ' 功能描述： 
    '     grsw_jieshou.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswJieshou
        Implements IDisposable

        Private m_strhtxtFILEQuery As String                      'htxtFILEQuery
        Private m_strhtxtFILERows As String                       'htxtFILERows
        Private m_strhtxtFILESort As String                       'htxtFILESort
        Private m_strhtxtFILESortColumnIndex As String            'htxtFILESortColumnIndex
        Private m_strhtxtFILESortType As String                   'htxtFILESortType

        Private m_strhtxtDivLeftFILE As String                    'htxtDivLeftFILE
        Private m_strhtxtDivTopFILE As String                     'htxtDivTopFILE
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtFILESessionIdQuery As String             'htxtFILESessionIdQuery

        Private m_strtxtFILEPageIndex As String                   'txtFILEPageIndex
        Private m_strtxtFILEPageSize As String                    'txtFILEPageSize

        Private m_intSelectedIndex_ddlYJR As Integer              'ddlYJR

        Private m_intSelectedIndex_ddlWJLX As Integer             'ddlWJLX
        Private m_intSelectedIndex_ddlSFJS As Integer             'ddlSFJS
        Private m_strtxtFILESearch_WJNDMin As String              'txtFILESearch_WJNDMin
        Private m_strtxtFILESearch_WJNDMax As String              'txtFILESearch_WJNDMax

        Private m_intPageSize_grdFILE As Integer
        Private m_intSelectedIndex_grdFILE As Integer
        Private m_intCurrentPageIndex_grdFILE As Integer











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strhtxtFILEQuery = ""
            m_strhtxtFILERows = ""
            m_strhtxtFILESort = ""
            m_strhtxtFILESortColumnIndex = ""
            m_strhtxtFILESortType = ""

            m_strhtxtDivLeftFILE = ""
            m_strhtxtDivTopFILE = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtFILESessionIdQuery = ""

            m_strtxtFILEPageIndex = ""
            m_strtxtFILEPageSize = ""

            m_intSelectedIndex_ddlYJR = -1

            m_intSelectedIndex_ddlWJLX = -1
            m_intSelectedIndex_ddlSFJS = -1
            m_strtxtFILESearch_WJNDMin = ""
            m_strtxtFILESearch_WJNDMax = ""

            m_intPageSize_grdFILE = 0
            m_intCurrentPageIndex_grdFILE = 0
            m_intSelectedIndex_grdFILE = -1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswJieshou)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' htxtFILEQuery属性
        '----------------------------------------------------------------
        Public Property htxtFILEQuery() As String
            Get
                htxtFILEQuery = m_strhtxtFILEQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILEQuery = Value
                Catch ex As Exception
                    m_strhtxtFILEQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILERows属性
        '----------------------------------------------------------------
        Public Property htxtFILERows() As String
            Get
                htxtFILERows = m_strhtxtFILERows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILERows = Value
                Catch ex As Exception
                    m_strhtxtFILERows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILESort属性
        '----------------------------------------------------------------
        Public Property htxtFILESort() As String
            Get
                htxtFILESort = m_strhtxtFILESort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESort = Value
                Catch ex As Exception
                    m_strhtxtFILESort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILESortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtFILESortColumnIndex() As String
            Get
                htxtFILESortColumnIndex = m_strhtxtFILESortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtFILESortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILESortType属性
        '----------------------------------------------------------------
        Public Property htxtFILESortType() As String
            Get
                htxtFILESortType = m_strhtxtFILESortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESortType = Value
                Catch ex As Exception
                    m_strhtxtFILESortType = ""
                End Try
            End Set
        End Property













        '----------------------------------------------------------------
        ' htxtDivLeftFILE属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftFILE() As String
            Get
                htxtDivLeftFILE = m_strhtxtDivLeftFILE
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftFILE = Value
                Catch ex As Exception
                    m_strhtxtDivLeftFILE = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopFILE属性
        '----------------------------------------------------------------
        Public Property htxtDivTopFILE() As String
            Get
                htxtDivTopFILE = m_strhtxtDivTopFILE
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopFILE = Value
                Catch ex As Exception
                    m_strhtxtDivTopFILE = ""
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
        ' htxtFILESessionIdQuery属性
        '----------------------------------------------------------------
        Public Property htxtFILESessionIdQuery() As String
            Get
                htxtFILESessionIdQuery = m_strhtxtFILESessionIdQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESessionIdQuery = Value
                Catch ex As Exception
                    m_strhtxtFILESessionIdQuery = ""
                End Try
            End Set
        End Property













        '----------------------------------------------------------------
        ' txtFILEPageIndex属性
        '----------------------------------------------------------------
        Public Property txtFILEPageIndex() As String
            Get
                txtFILEPageIndex = m_strtxtFILEPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILEPageIndex = Value
                Catch ex As Exception
                    m_strtxtFILEPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILEPageSize属性
        '----------------------------------------------------------------
        Public Property txtFILEPageSize() As String
            Get
                txtFILEPageSize = m_strtxtFILEPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILEPageSize = Value
                Catch ex As Exception
                    m_strtxtFILEPageSize = ""
                End Try
            End Set
        End Property












        '----------------------------------------------------------------
        ' ddlYJR_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlYJR_SelectedIndex() As Integer
            Get
                ddlYJR_SelectedIndex = m_intSelectedIndex_ddlYJR
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlYJR = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlYJR = -1
                End Try
            End Set
        End Property












        '----------------------------------------------------------------
        ' ddlWJLX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlWJLX_SelectedIndex() As Integer
            Get
                ddlWJLX_SelectedIndex = m_intSelectedIndex_ddlWJLX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlWJLX = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlWJLX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlSFJS_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlSFJS_SelectedIndex() As Integer
            Get
                ddlSFJS_SelectedIndex = m_intSelectedIndex_ddlSFJS
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlSFJS = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlSFJS = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_WJNDMin属性
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJNDMin() As String
            Get
                txtFILESearch_WJNDMin = m_strtxtFILESearch_WJNDMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILESearch_WJNDMin = Value
                Catch ex As Exception
                    m_strtxtFILESearch_WJNDMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_WJNDMax属性
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJNDMax() As String
            Get
                txtFILESearch_WJNDMax = m_strtxtFILESearch_WJNDMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILESearch_WJNDMax = Value
                Catch ex As Exception
                    m_strtxtFILESearch_WJNDMax = ""
                End Try
            End Set
        End Property











        '----------------------------------------------------------------
        ' grdFILEPageSize属性
        '----------------------------------------------------------------
        Public Property grdFILEPageSize() As Integer
            Get
                grdFILEPageSize = m_intPageSize_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdFILE = Value
                Catch ex As Exception
                    m_intPageSize_grdFILE = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFILECurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdFILECurrentPageIndex() As Integer
            Get
                grdFILECurrentPageIndex = m_intCurrentPageIndex_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdFILE = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdFILE = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFILESelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdFILESelectedIndex() As Integer
            Get
                grdFILESelectedIndex = m_intSelectedIndex_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdFILE = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdFILE = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
