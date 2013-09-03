Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IGgxxFujian
    '
    ' 功能描述： 
    '     flow_fujian.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgxxFujian
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strWJBS_I As String                                '文件标识
        Private m_objDataSet_FJ_I As Xydc.Platform.Common.Data.ggxxDianzigonggaoData '附件数据
        Private m_objDataSet_FJ_ZCML_I As Xydc.Platform.Common.Data.ggxxDianzigonggaoData       '资产目录附件数据
        Private m_blnEditMode_I As Boolean                           '编辑模式
        Private m_blnTrackRevisions_I As Boolean                     '文件支持痕迹记录?
        Private m_blnAutoSave_I As Boolean                           '退出时自动保存附件到数据库
        Private m_blnEnforeEdit_I As Boolean                         '是否定稿后修改?

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        '输出变动后的附件数据 = m_objDataSet_FJ_I









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_blnEditMode_I = False
            m_strWJBS_I = ""
            m_objDataSet_FJ_I = Nothing
            m_blnTrackRevisions_I = False
            m_blnAutoSave_I = False
            m_blnEnforeEdit_I = False
            m_objDataSet_FJ_ZCML_I = Nothing


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
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgxxFujian)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iWJBS属性
        '----------------------------------------------------------------
        Public Property iWJBS() As String
            Get
                iWJBS = m_strWJBS_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJBS_I = Value
                Catch ex As Exception
                    m_strWJBS_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iEditMode属性
        '----------------------------------------------------------------
        Public Property iEditMode() As Boolean
            Get
                iEditMode = m_blnEditMode_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnEditMode_I = Value
                Catch ex As Exception
                    m_blnEditMode_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iTrackRevisions属性
        '----------------------------------------------------------------
        Public Property iTrackRevisions() As Boolean
            Get
                iTrackRevisions = m_blnTrackRevisions_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnTrackRevisions_I = Value
                Catch ex As Exception
                    m_blnTrackRevisions_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iAutoSave属性
        '----------------------------------------------------------------
        Public Property iAutoSave() As Boolean
            Get
                iAutoSave = m_blnAutoSave_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAutoSave_I = Value
                Catch ex As Exception
                    m_blnAutoSave_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iEnforeEdit属性
        '----------------------------------------------------------------
        Public Property iEnforeEdit() As Boolean
            Get
                iEnforeEdit = m_blnEnforeEdit_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnEnforeEdit_I = Value
                Catch ex As Exception
                    m_blnEnforeEdit_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDataSet_FJ属性
        '----------------------------------------------------------------
        Public Property iDataSet_FJ() As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Get
                iDataSet_FJ = m_objDataSet_FJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.ggxxDianzigonggaoData)
                Try
                    m_objDataSet_FJ_I = Value
                Catch ex As Exception
                    m_objDataSet_FJ_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDataSet_FJ_ZCML属性
        '----------------------------------------------------------------
        Public Property iDataSet_FJ_ZCML() As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Get
                iDataSet_FJ_ZCML = m_objDataSet_FJ_ZCML_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.ggxxDianzigonggaoData)
                Try
                    m_objDataSet_FJ_ZCML_I = Value
                Catch ex As Exception
                    m_objDataSet_FJ_ZCML_I = Nothing
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' oDataSet_FJ属性
        '----------------------------------------------------------------
        Public Property oDataSet_FJ() As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Get
                oDataSet_FJ = m_objDataSet_FJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.ggxxDianzigonggaoData)
                Try
                    m_objDataSet_FJ_I = Value
                Catch ex As Exception
                    m_objDataSet_FJ_I = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
