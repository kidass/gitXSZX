Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IDmxzZtc
    '
    ' 功能描述： 
    '     dmxz_ztc.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IDmxzZtc
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_blnMultiSelect_I As Boolean             '允许多选(默认True-允许)
        Private m_blnAllowInput_I As Boolean              '是否允许手工输入(默认True-允许)
        Private m_blnAllowNull_I As Boolean               '允许空输入(默认True-允许)
        Private m_strZTC_I As String                      '当前输入主题词信息

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '返回方式：True-确定，False-取消
        Private m_objDataSet_O As System.Data.DataSet     '多选时返回的数据集
        Private m_strZTC_O As String                      '返回输入主题词信息









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_blnAllowInput_I = True
            m_blnAllowNull_I = True
            m_blnMultiSelect_I = True
            m_strZTC_I = ""

            '初始化输出参数
            m_blnExitMode_O = False
            m_strZTC_O = ""
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
            '释放资源
            If Not (m_objDataSet_O Is Nothing) Then
                m_objDataSet_O.Dispose()
                m_objDataSet_O = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IDmxzZtc)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












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
        ' iZTC属性
        '----------------------------------------------------------------
        Public Property iZTC() As String
            Get
                iZTC = m_strZTC_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZTC_I = Value
                Catch ex As Exception
                    m_strZTC_I = ""
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
        ' oZTC属性
        '----------------------------------------------------------------
        Public Property oZTC() As String
            Get
                oZTC = m_strZTC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZTC_O = Value
                Catch ex As Exception
                    m_strZTC_O = ""
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
