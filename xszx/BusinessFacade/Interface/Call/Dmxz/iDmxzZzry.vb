Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IDmxzZzry
    '
    ' 功能描述： 
    '     dmxz_zzry.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IDmxzZzry
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_blnDifferentFrame_I As Boolean       '调用帧与当前帧不同（跨帧调用）
        Private m_blnSelectMode_I As Boolean           '选择方式：false-人员(仅选择人员)，true-范围(可以选择人员、部门、范围)
        Private m_strInputList_I As String             '现有人员列表
        Private m_blnMultiSelect_I As Boolean          '可以多重选择?false-不能,true-能(缺省)
        Private m_blnSelectFFFW_I As Boolean           '可以选择范围?false-不能,true-能(缺省)
        Private m_blnSelectBMMC_I As Boolean           '可以选择部门?false-不能,true-能(缺省)
        Private m_blnAllowInput_I As Boolean           '可以手工输入?false-不能,true-能(缺省)
        Private m_blnAllowNull_I As Boolean            '允许空输入?false-不能,true-能(缺省)
        Private m_blnRestrictList_I As Boolean         '启用人员信息限制?false-不启用(缺省),true-启用
        Private m_strRestrictListSQL_I As String       '人员信息列表限制的SQL语句
        Private m_blnSendRestrict_I As Boolean         '是否打开发送限制?false-不打开(缺省),true-打开
        Private m_strCurrentBlr_I As String            '当前办理人的人员姓名
        Private m_strCurrentBlr_Dlr_I As String        '当前办理人的代理人姓名
        Private m_strWeituoren_I As String             '当前办理人受Weituoren委托来办理业务(姓名)

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean             '退出方式：True-确定，False-取消
        Private m_strRYList_O As String                '新选择的人员列表,按系统指定的分隔符分隔(CharSeparate)
        Private m_objDataSet_O As System.Data.DataSet  '多选时返回的数据集









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_blnSelectMode_I = True
            m_strInputList_I = ""
            m_blnMultiSelect_I = True
            m_blnSelectFFFW_I = True
            m_blnSelectBMMC_I = True
            m_blnAllowInput_I = True
            m_blnAllowNull_I = True
            m_blnRestrictList_I = False
            m_strRestrictListSQL_I = ""
            m_blnSendRestrict_I = False
            m_strCurrentBlr_I = ""
            m_strCurrentBlr_Dlr_I = ""
            m_strWeituoren_I = ""
            m_blnDifferentFrame_I = False

            '初始化输出参数
            m_blnExitMode_O = False
            m_strRYList_O = ""
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
            '释放本身资源
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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IDmxzZzry)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' iSelectMode属性
        '----------------------------------------------------------------
        Public Property iSelectMode() As Boolean
            Get
                iSelectMode = m_blnSelectMode_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSelectMode_I = Value
                Catch ex As Exception
                    m_blnSelectMode_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iRenyuanList属性
        '----------------------------------------------------------------
        Public Property iRenyuanList() As String
            Get
                iRenyuanList = m_strInputList_I
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
        ' iSelectBMMC属性
        '----------------------------------------------------------------
        Public Property iSelectBMMC() As Boolean
            Get
                iSelectBMMC = m_blnSelectBMMC_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSelectBMMC_I = Value
                Catch ex As Exception
                    m_blnSelectBMMC_I = False
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
        ' iRestrictRenyuanList属性
        '----------------------------------------------------------------
        Public Property iRestrictRenyuanList() As Boolean
            Get
                iRestrictRenyuanList = m_blnRestrictList_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnRestrictList_I = Value
                Catch ex As Exception
                    m_blnRestrictList_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iRestrictRenyuanListSQL属性
        '----------------------------------------------------------------
        Public Property iRestrictRenyuanListSQL() As String
            Get
                iRestrictRenyuanListSQL = m_strRestrictListSQL_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRestrictListSQL_I = Value
                Catch ex As Exception
                    m_strRestrictListSQL_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSendRestrict属性
        '----------------------------------------------------------------
        Public Property iSendRestrict() As Boolean
            Get
                iSendRestrict = m_blnSendRestrict_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSendRestrict_I = Value
                Catch ex As Exception
                    m_blnSendRestrict_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCurrentBlr属性
        '----------------------------------------------------------------
        Public Property iCurrentBlr() As String
            Get
                iCurrentBlr = m_strCurrentBlr_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strCurrentBlr_I = Value
                Catch ex As Exception
                    m_strCurrentBlr_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCurrentBlrDlr属性
        '----------------------------------------------------------------
        Public Property iCurrentBlrDlr() As String
            Get
                iCurrentBlrDlr = m_strCurrentBlr_Dlr_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strCurrentBlr_Dlr_I = Value
                Catch ex As Exception
                    m_strCurrentBlr_Dlr_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWeiTuoRen属性
        '----------------------------------------------------------------
        Public Property iWeiTuoRen() As String
            Get
                iWeiTuoRen = m_strWeituoren_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWeituoren_I = Value
                Catch ex As Exception
                    m_strWeituoren_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDifferentFrame属性
        '----------------------------------------------------------------
        Public Property iDifferentFrame() As Boolean
            Get
                iDifferentFrame = m_blnDifferentFrame_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnDifferentFrame_I = Value
                Catch ex As Exception
                    m_blnDifferentFrame_I = False
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
        ' oRenyuanList属性
        '----------------------------------------------------------------
        Public Property oRenyuanList() As String
            Get
                oRenyuanList = m_strRYList_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRYList_O = Value
                Catch ex As Exception
                    m_strRYList_O = ""
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
