'----------------------------------------------------------------
' Copyright (C) 2006-2016 Josco Software Corporation
' All rights reserved.
'
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation. See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY 
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT 
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR 
' FITNESS FOR A PARTICULAR PURPOSE.
'----------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports System

Namespace Xydc.Platform.BusinessRules

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessRules
    ' ����    ��IRulesFlowObjectCreate
    '
    ' ����������
    '     IRulesFlowObject�����ӿ�
    '----------------------------------------------------------------
    Public Interface IRulesFlowObjectCreate

        'IRulesFlowObject���󴴽���
        Function Create(ByVal strFlowType As String, ByVal strFlowTypeName As String) As Xydc.Platform.BusinessRules.rulesFlowObject

    End Interface

End Namespace
