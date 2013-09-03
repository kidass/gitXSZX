Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMDeepData_monthCompute
    '
    ' 功能描述： 
    '     deepData_monthCompute.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMDeepData_monthCompute
        Implements IDisposable

        Private m_strhtxtComputeQuery As String             'htxtComputeQuery
        Private m_strhtxtComputeQuery_0 As String             'htxtComputeQuery
        Private m_strhtxtType As String                     'htxtType
        Private m_strhtxtSessionIdQuery As String           'htxtSessionIdQuery
        Private m_strhtxtStartDate As String                'htxtStartDate
        Private m_strhtxtEndDate As String                  'htxtEndDate
        Private m_strhtxtHouseType As String                'htxtHouseType

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strhtxtComputeQuery = ""
            m_strhtxtComputeQuery_0 = ""
            m_strhtxtType = ""
            m_strhtxtSessionIdQuery = "" '
            m_strhtxtStartDate = ""
            m_strhtxtEndDate = ""
            m_strhtxtHouseType = ""
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMDeepData_monthCompute)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub







        '----------------------------------------------------------------
        ' htxtHouseType属性
        '----------------------------------------------------------------
        Public Property htxtHouseType() As String
            Get
                htxtHouseType = m_strhtxtHouseType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtHouseType = Value
                Catch ex As Exception
                    m_strhtxtHouseType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtStartDate属性
        '----------------------------------------------------------------
        Public Property htxtStartDate() As String
            Get
                htxtStartDate = m_strhtxtStartDate
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtStartDate = Value
                Catch ex As Exception
                    m_strhtxtStartDate = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtEndDate属性
        '----------------------------------------------------------------
        Public Property htxtEndDate() As String
            Get
                htxtEndDate = m_strhtxtEndDate
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtEndDate = Value
                Catch ex As Exception
                    m_strhtxtEndDate = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtComputeQuery_0属性
        '----------------------------------------------------------------
        Public Property htxtComputeQuery_0() As String
            Get
                htxtComputeQuery_0 = m_strhtxtComputeQuery_0
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtComputeQuery_0 = Value
                Catch ex As Exception
                    m_strhtxtComputeQuery_0 = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtComputeQuery属性
        '----------------------------------------------------------------
        Public Property htxtComputeQuery() As String
            Get
                htxtComputeQuery = m_strhtxtComputeQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtComputeQuery = Value
                Catch ex As Exception
                    m_strhtxtComputeQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtType属性
        '----------------------------------------------------------------
        Public Property htxtType() As String
            Get
                htxtType = m_strhtxtType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtType = Value
                Catch ex As Exception
                    m_strhtxtType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSessionIdQuery属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdQuery() As String
            Get
                htxtSessionIdQuery = m_strhtxtSessionIdQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdQuery = ""
                End Try
            End Set
        End Property





    End Class
End Namespace
