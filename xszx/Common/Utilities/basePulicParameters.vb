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

Imports Microsoft.VisualBasic

Imports System
Imports System.Data
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Utilities

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.Common.Utilities
    ' 类名    ：PulicParameters
    '
    ' 功能描述：
    '     定义系统用的全局参数信息
    '----------------------------------------------------------------
    Public Class PulicParameters
        Implements IDisposable

        '定义数据加密用的非对称密钥对
        Private m_bKey() As Byte = {&H15, &H28, &H34, &H41, &H85, &H36, &H77, &H28, &H19, &H10, &H51, &H62, &H93, &H64, &H37, &H52}
        Private m_bIV() As Byte = {&H61, &H22, &H53, &H74, &H25, &H66, &H97, &H98, &H29, &H55, &H46, &H73, &H19, &H43, &H77, &H31}

        '定义排序命令enum
        Public Enum enumSortType
            Asc = 1
            Desc = 2
            None = 3
        End Enum

        '定义编辑模式
        Public Enum enumEditType
            eSelect = 0
            eAddNew = 1
            eUpdate = 2
            eDelete = 3
            eCpyNew = 4
        End Enum

        '搜索设置
        Public Const SearchConfig As Boolean = True

        '多值分隔符
        Public Const CharSeparate As String = ","
        '表示True的字符
        Public Const CharTrue As String = "√"
        '表示False的字符
        Public Const CharFalse As String = "×"
        '数据行指针显示符
        Public Const CharPointer As String = "＊"
        '字符横向箭头
        Public Const CharArrow As String = "→"
        '升序排列字符
        Public Const CharAsc As String = "↑"
        '降序排列字符
        Public Const CharDesc As String = "↓"
        '分级代码分隔符
        Public Const CharFjdmSeparate As String = "."

        '文件字号左括弧
        Public Const CharWjzhLf As String = "〔"
        '文件字号右括弧
        Public Const CharWjzhRt As String = "〕"

        '文档保护密码
        Public Const FileProtectPassword As String = "12345678"

        'Request请求中的CheckBox选择状态值
        Public Const CheckBoxCheckedValue As String = "on"

        '模块之间调用时QueryString中用到的SessionId定义
        '定义模块输入参数用的SessionId
        Public Const ISessionId As String = "iSessionId"
        '定义返回到调用模块时的SessionId
        Public Const OSessionId As String = "oSessionId"
        '定义保存模块自身运行环境用的SessionId
        Public Const MSessionId As String = "mSessionId"

        '备份文件的缺省后缀
        Public Const BACKUPFILEEXT As String = ".bak"









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' 析构函数实现
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Utilities.PulicParameters)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' 创建GUID
        '----------------------------------------------------------------
        Public Function getNewGuid() As String

            Try
                getNewGuid = System.Guid.NewGuid().ToString()
                getNewGuid = getNewGuid.ToUpper()
            Catch ex As Exception
                getNewGuid = ""
            End Try

        End Function










        '----------------------------------------------------------------
        ' 获取Unicode的字符串转换为MBCS字符串的字节长度
        '----------------------------------------------------------------
        Public Function getStringLength(ByVal strValue As String) As Integer

            Try
                Dim bSrc() As Byte
                bSrc = System.Text.Encoding.Unicode.GetBytes(strValue)
                Dim bDes() As Byte
                bDes = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.GetEncoding("GB2312"), bSrc)
                getStringLength = bDes.Length()
            Catch ex As Exception
                getStringLength = strValue.Length
            End Try

        End Function

        '----------------------------------------------------------------
        ' 从Unicode的字符串中获取指定长度的字符串，长度按MBCS计算
        '----------------------------------------------------------------
        Public Function getSubString(ByVal strValue As String, ByVal intLen As Integer) As String

            Try
                '计算MBCS字节数据
                Dim bSrc() As Byte
                bSrc = System.Text.Encoding.Unicode.GetBytes(strValue)
                Dim bDes() As Byte
                bDes = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.GetEncoding("GB2312"), bSrc)
                '从intLen之后清空
                If bDes.Length > intLen Then
                    Dim bTmp(intLen - 1) As Byte
                    Dim i As Integer
                    For i = 0 To intLen - 1 Step 1
                        '最后是否为双字节？
                        If i = intLen - 1 Then
                            If bDes(i) >= 128 Then
                                bTmp(i) = 0
                            Else
                                bTmp(i) = bDes(i)
                            End If
                        Else
                            bTmp(i) = bDes(i)
                        End If
                    Next
                    Dim bFin() As Byte
                    bFin = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding("GB2312"), System.Text.Encoding.Unicode, bTmp)
                    getSubString = System.Text.Encoding.Unicode.GetString(bFin)
                Else
                    getSubString = strValue
                End If
            Catch ex As Exception
                getSubString = strValue
            End Try

        End Function









        '----------------------------------------------------------------
        ' 是否为Integer
        '----------------------------------------------------------------
        Public Function isIntegerString(ByVal strValue As String) As Boolean

            Dim intValue As Integer
            isIntegerString = False
            Try
                intValue = CType(strValue, Integer)
                isIntegerString = True
            Catch ex As Exception
                isIntegerString = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 是否为全数字字符串
        '----------------------------------------------------------------
        Public Function isNumericString(ByVal strValue As String) As Boolean

            Dim dblValue As Double
            isNumericString = False
            Try
                dblValue = CType(strValue, Double)
                If strValue.IndexOf(",") > 0 Then
                    Exit Try
                End If
                If strValue.IndexOf(".") > 0 Then
                    Exit Try
                End If
                isNumericString = True
            Catch ex As Exception
                isNumericString = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 是否为浮点型值
        '----------------------------------------------------------------
        Public Function isFloatString(ByVal strValue As String) As Boolean

            Dim dblValue As Double
            Try
                dblValue = CType(strValue, Double)
                isFloatString = True
            Catch ex As Exception
                isFloatString = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 是否为日期型值
        '----------------------------------------------------------------
        Public Function isDatetimeString(ByVal strValue As String) As Boolean

            Dim dateValue As DateTime
            Try
                dateValue = CType(strValue, DateTime)
                isDatetimeString = True
            Catch ex As Exception
                isDatetimeString = False
            End Try

        End Function









        '----------------------------------------------------------------
        ' 安全获取对象的值：String 版本(裁减空格)
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As String) As String

            Try
                getObjectValue = CType(value, String)
                If getObjectValue.Length > 0 Then
                    getObjectValue = getObjectValue.Trim()
                End If
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的值：String 版本(不裁减空格)
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As String, ByVal blnNoTrim As Boolean) As String

            Try
                getObjectValue = CType(value, String)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的值：DateTime 版本
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As System.DateTime) As System.DateTime

            Try
                getObjectValue = CType(value, System.DateTime)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的值：DateTime 版本
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Dim objDateTime As System.DateTime
            Try
                objDateTime = CType(value, System.DateTime)
                getObjectValue = objDateTime.ToString(strFormat)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的值：Integer 版本
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Integer) As Integer

            Try
                getObjectValue = CType(value, Integer)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的值：Long 版本
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Long) As Long

            Try
                getObjectValue = CType(value, Long)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的值：double 版本
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Double) As Double

            Try
                getObjectValue = CType(value, Double)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的值：Boolean 版本
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Boolean) As Boolean

            Try
                getObjectValue = CType(value, Boolean)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的值：Byte() 版本
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Byte()) As Byte()

            Try
                getObjectValue = CType(value, Byte())
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function










        '----------------------------------------------------------------
        ' 安全获取对象的显示值：DateTime 版本
        '----------------------------------------------------------------
        Public Function getDisplayValue( _
            ByVal value As Object, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Dim objDateTime As System.DateTime
            Try
                objDateTime = CType(value, System.DateTime)
                If objDateTime.Year <= 1900 Then
                    getDisplayValue = defvalue
                Else
                    getDisplayValue = objDateTime.ToString(strFormat)
                End If
            Catch ex As Exception
                getDisplayValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的显示值：Integer 版本
        '----------------------------------------------------------------
        Public Function getDisplayValue( _
            ByVal value As Integer, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Try
                If value = 0 Then
                    getDisplayValue = ""
                Else
                    If strFormat = "" Then
                        getDisplayValue = value.ToString
                    Else
                        getDisplayValue = value.ToString(strFormat)
                    End If
                End If
            Catch ex As Exception
                getDisplayValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的显示值：Long 版本
        '----------------------------------------------------------------
        Public Function getDisplayValue( _
            ByVal value As Long, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Try
                If value = 0 Then
                    getDisplayValue = ""
                Else
                    If strFormat = "" Then
                        getDisplayValue = value.ToString
                    Else
                        getDisplayValue = value.ToString(strFormat)
                    End If
                End If
            Catch ex As Exception
                getDisplayValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' 安全获取对象的显示值：double 版本
        '----------------------------------------------------------------
        Public Function getDisplayValue( _
            ByVal value As Double, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Try
                If value = 0 Then
                    getDisplayValue = ""
                Else
                    If strFormat = "" Then
                        getDisplayValue = value.ToString
                    Else
                        getDisplayValue = value.ToString(strFormat)
                    End If
                End If
            Catch ex As Exception
                getDisplayValue = defvalue
            End Try

        End Function











        '----------------------------------------------------------------
        ' 根据给定长度获取对应分级代码的级别，从1级开始
        '     intFJCDSM     ：分级代码各级总长度
        '     intCodeLen    ：要检测的代码长度
        ' 返回
        '                   ：指定级别(-1表示错误)
        '----------------------------------------------------------------
        Public Function getCodeLevel( _
            ByVal intFJCDSM() As Integer, _
            ByVal intCodeLen As Integer) As Integer

            Try
                If intCodeLen = 0 Then
                    getCodeLevel = -1
                Else
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = intFJCDSM.Length
                    For i = 0 To intCount - 1 Step 1
                        If intFJCDSM(i) = intCodeLen Then
                            getCodeLevel = i + 1
                            Exit Try
                        End If
                    Next
                End If
            Catch ex As Exception
                getCodeLevel = -1
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据给定长度获取对应分级代码的级别，从1级开始
        '     strCodeValue  ：代码值
        '     strCodeSep    ：代码分隔符
        ' 返回
        '                   ：指定级别(-1表示错误)
        '----------------------------------------------------------------
        Public Function getCodeLevel( _
            ByVal strCodeValue As String, _
            ByVal strCodeSep As String) As Integer

            Dim intLevel As Integer = -1

            Try
                If strCodeValue Is Nothing Then strCodeValue = ""
                strCodeValue = strCodeValue.Trim()

                If strCodeValue <> "" Then
                    Dim strIndex() As String
                    strIndex = strCodeValue.Split(strCodeSep.ToCharArray())
                    intLevel = strIndex.Length
                End If
            Catch ex As Exception
            End Try

            getCodeLevel = intLevel

        End Function

        '----------------------------------------------------------------
        ' 检验代码长度的合法性
        '     intFJCDSM     ：分级代码各级总长度
        '     intCodeLen    ：要检测的代码长度
        ' 返回
        '     True          ：合法
        '     False         ：不合法
        '----------------------------------------------------------------
        Public Function doVerifyCodeLength( _
            ByVal intFJCDSM() As Integer, _
            ByVal intCodeLen As Integer) As Boolean

            doVerifyCodeLength = False
            Try
                If intCodeLen = 0 Then
                    doVerifyCodeLength = True
                Else
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = intFJCDSM.Length
                    For i = 0 To intCount - 1 Step 1
                        If intFJCDSM(i) = intCodeLen Then
                            doVerifyCodeLength = True
                            Exit Try
                        End If
                    Next
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据给定代码获取上级代码
        '     intFJCDSM     ：分级代码各级总长度
        '     strCode       ：当前代码
        ' 返回
        '                   ：上级代码值
        '----------------------------------------------------------------
        Public Function getPrevLevelCode( _
            ByVal intFJCDSM() As Integer, _
            ByVal strCode As String) As String

            Dim strValue As String = ""

            Try
                If strCode Is Nothing Then strCode = ""
                strCode = strCode.Trim()

                If strCode <> "" Then
                    Dim intCount As Integer
                    Dim intLen As Integer
                    Dim i As Integer
                    intLen = strCode.Length
                    intCount = intFJCDSM.Length
                    For i = 0 To intCount - 1 Step 1
                        If intLen = intFJCDSM(i) Then
                            If i > 0 Then
                                strValue = strCode.Substring(0, intFJCDSM(i - 1))
                            End If
                            Exit Try
                        End If
                    Next
                End If
            Catch ex As Exception
            End Try

            getPrevLevelCode = strValue

        End Function

        '----------------------------------------------------------------
        ' 根据给定代码获取上级代码
        '     strCodeValue  ：代码值
        '     strCodeSep    ：代码分隔符
        ' 返回
        '                   ：上级代码值
        '----------------------------------------------------------------
        Public Function getPrevLevelCode( _
            ByVal strCodeValue As String, _
            ByVal strCodeSep As String) As String

            Dim strValue As String = ""

            Try
                If strCodeValue Is Nothing Then strCodeValue = ""
                If strCodeSep Is Nothing Then strCodeSep = ""
                strCodeValue = strCodeValue.Trim()
                strCodeSep = strCodeSep.Trim()

                If strCodeValue <> "" Then
                    Dim strIndex() As String
                    Dim intCount As Integer
                    Dim i As Integer
                    strIndex = strCodeValue.Split(strCodeSep.ToCharArray())
                    intCount = strIndex.Length
                    For i = 0 To intCount - 2 Step 1
                        If strValue = "" Then
                            strValue = strIndex(i)
                        Else
                            strValue = strValue + strCodeSep + strIndex(i)
                        End If
                    Next
                End If
            Catch ex As Exception
            End Try

            getPrevLevelCode = strValue

        End Function

        '----------------------------------------------------------------
        ' 根据给定strCodeValue获取指定级别的代码值
        '     strCodeValue  ：代码值
        '     strCodeSep    ：代码分隔符
        '     intLevel      ：代码级别
        ' 返回
        '                   ：指定级别的本级代码
        '----------------------------------------------------------------
        Public Function getCodeValue( _
            ByVal strCodeValue As String, _
            ByVal strCodeSep As String, _
            ByVal intLevel As Integer) As String

            Dim strValue As String = ""

            Try
                If strCodeValue Is Nothing Then strCodeValue = ""
                If strCodeSep Is Nothing Then strCodeSep = ""
                strCodeValue = strCodeValue.Trim()
                strCodeSep = strCodeSep.Trim()
                If intLevel < 1 Then Exit Try
                If strCodeValue = "" Then Exit Try

                Dim strIndex() As String
                Dim intCount As Integer
                Dim i As Integer
                strIndex = strCodeValue.Split(strCodeSep.ToCharArray())
                If intLevel - 1 > strIndex.Length Then Exit Try
                strValue = strIndex(intLevel - 1)
            Catch ex As Exception
            End Try

            getCodeValue = strValue

        End Function

        '----------------------------------------------------------------
        ' 根据给定strCodeValue获取指定级别的代码值
        '     strCodeValue  ：代码值
        '     strCodeSep    ：代码分隔符
        '     intLevel      ：代码级别
        '     blnUnused     ：重载用
        ' 返回
        '                   ：指定级别的完全代码
        '----------------------------------------------------------------
        Public Function getCodeValue( _
            ByVal strCodeValue As String, _
            ByVal strCodeSep As String, _
            ByVal intLevel As Integer, _
            ByVal blnUnused As Boolean) As String

            Dim strValue As String = ""

            Try
                If strCodeValue Is Nothing Then strCodeValue = ""
                If strCodeSep Is Nothing Then strCodeSep = ""
                strCodeValue = strCodeValue.Trim()
                strCodeSep = strCodeSep.Trim()
                If intLevel < 1 Then Exit Try
                If strCodeValue = "" Then Exit Try

                Dim strIndex() As String
                Dim intCount As Integer
                Dim i As Integer
                strIndex = strCodeValue.Split(strCodeSep.ToCharArray())
                If intLevel > strIndex.Length Then Exit Try
                For i = 0 To intLevel - 1 Step 1
                    If strValue = "" Then
                        strValue = strIndex(i)
                    Else
                        strValue = strValue + strCodeSep + strIndex(i)
                    End If
                Next
            Catch ex As Exception
            End Try

            getCodeValue = strValue

        End Function










        '----------------------------------------------------------------
        ' 在指定字符串前或后填充给定字符
        '     strSrc        ：源字符串
        '     intLen        ：填充后的字符串长度
        '     strFill       ：用来填充的字符
        '     blnFront      ：True-在前面填充，False-在后面填充
        ' 返回
        '                   ：填充后的字符串
        '----------------------------------------------------------------
        Public Function doFillString( _
            ByVal strSrc As String, _
            ByVal intLen As Integer, _
            ByVal strFill As String, _
            ByVal blnFront As Boolean) As String

            Dim strValue As String = ""
            Dim intCount As Integer
            Dim i As Integer

            Try
                If strSrc Is Nothing Then strSrc = ""
                If strFill Is Nothing Then strFill = ""
                strSrc = strSrc.Trim()
                strFill = strFill.Trim()

                intCount = intLen - strSrc.Length
                strValue = strSrc
                For i = 0 To intCount - 1 Step 1
                    If blnFront = True Then
                        strValue = strFill + strValue
                    Else
                        strValue = strValue + strFill
                    End If
                Next

                doFillString = strValue

            Catch ex As Exception
                doFillString = strSrc
            End Try

        End Function

        '----------------------------------------------------------------
        ' 将ANSI字符串转换为HTML表示的字符串
        '     strSrc        ：源字符串
        ' 返回
        '                   ：转换后的字符串
        '----------------------------------------------------------------
        Public Function doConvertToHtml(ByVal strSrc As String) As String

            Dim strTemp As String = ""
            Try
                If strSrc Is Nothing Then strSrc = ""
                strTemp = strSrc

                strTemp = strTemp.Replace(Chr(13), "<br>")
                strTemp = strTemp.Replace(" ", "&nbsp;&nbsp;")
                strTemp = strTemp.Replace(Chr(10), "")

                doConvertToHtml = strTemp

            Catch ex As Exception
                doConvertToHtml = strTemp
            End Try

        End Function

        '----------------------------------------------------------------
        ' 将字符串中的分隔符转换为标准分隔符
        '     strSrc        ：源字符串
        ' 返回
        '                   ：转换后的字符串
        '----------------------------------------------------------------
        Public Function doTranslateSeperate(ByVal strSrc As String) As String

            Dim strTemp As String = ""
            Try
                Dim strSep As String = Me.CharSeparate
                If strSrc Is Nothing Then strSrc = ""
                strTemp = strSrc

                strTemp = strTemp.Replace("，", strSep)
                strTemp = strTemp.Replace("；", strSep)
                strTemp = strTemp.Replace("：", strSep)
                strTemp = strTemp.Replace("、", strSep)

                strTemp = strTemp.Replace(";", strSep)
                strTemp = strTemp.Replace(":", strSep)

                doTranslateSeperate = strTemp

            Catch ex As Exception
                doTranslateSeperate = strTemp
            End Try

        End Function

        '----------------------------------------------------------------
        ' 将日期转化为字符串表示(1901-1-1 00:00:00以前的日期计为空日期)
        '     objDate       ：日期值
        '     strFormat     ：格式化字符串
        ' 返回
        '                   ：转换后的字符串
        '----------------------------------------------------------------
        Public Function doDateToString( _
            ByVal objDate As System.DateTime, _
            ByVal strFormat As String) As String

            Dim strTemp As String = ""
            Try
                If strFormat Is Nothing Then strFormat = ""
                strFormat = strFormat.Trim()
                If strFormat = "" Then Exit Try

                Dim objNullDate As New System.DateTime(1901, 1, 1, 0, 0, 0)
                If objDate < objNullDate Then
                Else
                    strTemp = Format(objDate, strFormat)
                End If
                doDateToString = strTemp

            Catch ex As Exception
                doDateToString = strTemp
            End Try

        End Function








        '----------------------------------------------------------------
        ' 用Rijndael算法加密文件数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFileSpec          ：要加密的文件的完整路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 备注
        '      加密完成后向加密后的文件写入特定加密头信息
        '     已经加密的文件不再执行加密
        '----------------------------------------------------------------
        Public Function doEncryptFile( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

            Dim objICryptoTransform As System.Security.Cryptography.ICryptoTransform = Nothing
            Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged = Nothing
            Dim objCryptoStream As System.Security.Cryptography.CryptoStream = Nothing
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objSrcFileStream As System.IO.FileStream = Nothing
            Dim objSrcFileInfo As System.IO.FileInfo = Nothing
            Dim objDesFileStream As System.IO.FileStream = Nothing
            Dim objDesFileInfo As System.IO.FileInfo = Nothing
            Dim intBufSize As Integer = 1024 'Buffer Size(byte)
            Dim blnDelete As Boolean = False
            Dim strBakFile As String = ""

            doEncryptFile = False
            strErrMsg = ""

            Try
                '判断是否加密(32bytes)
                objSrcFileInfo = New System.IO.FileInfo(strFileSpec)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                Dim byteKey(Me.m_bKey.Length - 1) As Byte
                Dim byteIV(Me.m_bIV.Length - 1) As Byte
                objSrcFileStream.Read(byteKey, 0, Me.m_bKey.Length)
                objSrcFileStream.Read(byteIV, 0, Me.m_bIV.Length)
                Dim blnFound As Boolean = False
                Dim i As Integer = 0
                For i = 0 To byteKey.Length - 1 Step 1
                    If byteKey(i) <> Me.m_bKey(i) Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    For i = 0 To byteIV.Length - 1 Step 1
                        If byteIV(i) <> Me.m_bIV(i) Then
                            blnFound = True
                            Exit For
                        End If
                    Next
                End If
                If blnFound = False Then
                    '已经加密，不用加密
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                System.Threading.Thread.Sleep(15)

                '备份文件到strFileSpec + .org
                strBakFile = strFileSpec + ".org"
                If objBaseLocalFile.doCopyFile(strErrMsg, strFileSpec, strBakFile, True) = False Then
                    GoTo errProc
                End If
                blnDelete = True
                System.Threading.Thread.Sleep(15)

                '获取加密器
                objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged
                objICryptoTransform = objRijndaelManaged.CreateEncryptor(Me.m_bKey, Me.m_bIV)

                '创建加密数据流
                objDesFileInfo = New System.IO.FileInfo(strBakFile)
                objDesFileStream = objDesFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Write, System.IO.FileShare.None)
                objCryptoStream = New System.Security.Cryptography.CryptoStream( _
                    objDesFileStream, _
                    objICryptoTransform, _
                    System.Security.Cryptography.CryptoStreamMode.Write)

                '加密文件
                Dim byteSrc(intBufSize - 1) As Byte
                Dim intRead As Integer = 0
                objSrcFileInfo = New System.IO.FileInfo(strFileSpec)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                While True
                    intRead = objSrcFileStream.Read(byteSrc, 0, byteSrc.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    objCryptoStream.Write(byteSrc, 0, intRead)
                End While
                objCryptoStream.FlushFinalBlock()

                '关闭文件
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                System.Threading.Thread.Sleep(15)

                '写加密文件头(32bytes)
                objDesFileInfo = New System.IO.FileInfo(strFileSpec)
                objDesFileStream = objDesFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Write, System.IO.FileShare.None)
                objDesFileStream.Write(Me.m_bKey, 0, Me.m_bKey.Length)
                objDesFileStream.Write(Me.m_bIV, 0, Me.m_bIV.Length)
                objSrcFileInfo = New System.IO.FileInfo(strBakFile)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                While True
                    intRead = objSrcFileStream.Read(byteSrc, 0, byteSrc.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    objDesFileStream.Write(byteSrc, 0, intRead)
                End While
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                System.Threading.Thread.Sleep(15)

                '删除临时文件
                If blnDelete = True And strBakFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strBakFile) = False Then
                        '忽略
                    End If
                    System.Threading.Thread.Sleep(15)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)

            doEncryptFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)
            If blnDelete = True And strBakFile <> "" Then
                Dim strErrMsgA As String = ""
                objBaseLocalFile.doDeleteFile(strErrMsgA, strBakFile)
            End If
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 用Rijndael算法解密文件数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFileSpec          ：要解密的文件的完整路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 备注
        '      解密文件前检查是否有特定加密文件头
        '----------------------------------------------------------------
        Public Function doDecryptFile( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

            Dim objICryptoTransform As System.Security.Cryptography.ICryptoTransform = Nothing
            Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged = Nothing
            Dim objCryptoStream As System.Security.Cryptography.CryptoStream = Nothing
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objSrcFileStream As System.IO.FileStream = Nothing
            Dim objSrcFileInfo As System.IO.FileInfo = Nothing
            Dim objDesFileStream As System.IO.FileStream = Nothing
            Dim objDesFileInfo As System.IO.FileInfo = Nothing
            Dim intBufferSize As Integer = 1024 'buffer size (byte)
            Dim blnDelete As Boolean = False
            Dim strBakFile As String = ""

            doDecryptFile = False
            strErrMsg = ""

            Try
                Dim byteBuffer(intBufferSize - 1) As Byte
                Dim intRead As Integer = 0

                '判断是否加密(32bytes)
                objSrcFileInfo = New System.IO.FileInfo(strFileSpec)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                Dim byteKey(Me.m_bKey.Length - 1) As Byte
                Dim byteIV(Me.m_bIV.Length - 1) As Byte
                objSrcFileStream.Read(byteKey, 0, Me.m_bKey.Length)
                objSrcFileStream.Read(byteIV, 0, Me.m_bIV.Length)
                Dim blnFound As Boolean = False
                Dim i As Integer = 0
                For i = 0 To byteKey.Length - 1 Step 1
                    If byteKey(i) <> Me.m_bKey(i) Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    For i = 0 To byteIV.Length - 1 Step 1
                        If byteIV(i) <> Me.m_bIV(i) Then
                            blnFound = True
                            Exit For
                        End If
                    Next
                End If
                If blnFound = True Then
                    '没有加密，不用解密
                    Exit Try
                End If

                '写真实文件内容到strBakFile
                strBakFile = strFileSpec + ".org"
                objDesFileInfo = New System.IO.FileInfo(strBakFile)
                objDesFileStream = objDesFileInfo.Open(System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None)
                While True
                    intRead = objSrcFileStream.Read(byteBuffer, 0, byteBuffer.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    objDesFileStream.Write(byteBuffer, 0, intRead)
                End While
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
                blnDelete = True
                System.Threading.Thread.Sleep(15)

                '获取解密器
                objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged
                objICryptoTransform = objRijndaelManaged.CreateDecryptor(Me.m_bKey, Me.m_bIV)

                '创建解密数据流
                objSrcFileInfo = New System.IO.FileInfo(strBakFile)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                objCryptoStream = New System.Security.Cryptography.CryptoStream( _
                 objSrcFileStream, _
                 objICryptoTransform, _
                 System.Security.Cryptography.CryptoStreamMode.Read)

                '解密文件
                objDesFileInfo = New System.IO.FileInfo(strFileSpec)
                objDesFileStream = objDesFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Write, System.IO.FileShare.None)
                While True
                    intRead = objCryptoStream.Read(byteBuffer, 0, byteBuffer.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    objDesFileStream.Write(byteBuffer, 0, intRead)
                End While

                '关闭文件
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                System.Threading.Thread.Sleep(15)

                '删除临时文件
                If blnDelete = True And strBakFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strBakFile) = False Then
                        '忽略
                    End If
                    System.Threading.Thread.Sleep(15)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)

            doDecryptFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)
            If blnDelete = True And strBakFile <> "" Then
                Dim strErrMsgA As String = ""
                objBaseLocalFile.doDeleteFile(strErrMsgA, strBakFile)
            End If
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 用Rijndael算法加密字符串数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strData              ：要加密的字符串
        '     bData                ：加密后的字节数据(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doEncryptString( _
            ByRef strErrMsg As String, _
            ByVal strData As String, _
            ByRef bData() As Byte) As Boolean

            Dim objICryptoTransform As System.Security.Cryptography.ICryptoTransform = Nothing
            Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged = Nothing
            Dim objCryptoStream As System.Security.Cryptography.CryptoStream = Nothing
            Dim objMemoryStream As System.IO.MemoryStream = Nothing

            doEncryptString = False

            Try
                '获取加密器
                objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged
                objICryptoTransform = objRijndaelManaged.CreateEncryptor(Me.m_bKey, Me.m_bIV)

                '创建加密数据流
                objMemoryStream = New System.IO.MemoryStream
                objCryptoStream = New System.Security.Cryptography.CryptoStream( _
                    objMemoryStream, _
                    objICryptoTransform, _
                    System.Security.Cryptography.CryptoStreamMode.Write)

                '将字符串转换为字节数据
                Dim bSrc() As Byte = System.Text.Encoding.Unicode.GetBytes(strData)

                '将字节数据写入加密数据流
                objCryptoStream.Write(bSrc, 0, bSrc.Length)
                objCryptoStream.FlushFinalBlock()

                '获取加密后的数据
                bData = objMemoryStream.ToArray()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objMemoryStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)

            doEncryptString = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objMemoryStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 用Rijndael算法解密加密后的字节数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     bData                ：加密后的字节数据
        '     strData              ：要加密的字符串(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDecryptString( _
            ByRef strErrMsg As String, _
            ByVal bData() As Byte, _
            ByRef strData As String) As Boolean

            Dim objICryptoTransform As System.Security.Cryptography.ICryptoTransform = Nothing
            Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged = Nothing
            Dim objCryptoStream As System.Security.Cryptography.CryptoStream = Nothing
            Dim objMemoryStream As System.IO.MemoryStream = Nothing
            Dim objDesStream As System.IO.MemoryStream = Nothing

            doDecryptString = False
            strData = ""

            Try
                '检查
                If bData Is Nothing Then Exit Try
                If bData.Length < 1 Then Exit Try

                '获取解密器
                objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged
                objICryptoTransform = objRijndaelManaged.CreateDecryptor(Me.m_bKey, Me.m_bIV)

                '创建解密数据流
                objMemoryStream = New System.IO.MemoryStream(bData)
                objCryptoStream = New System.Security.Cryptography.CryptoStream( _
                 objMemoryStream, _
                 objICryptoTransform, _
                 System.Security.Cryptography.CryptoStreamMode.Read)

                '从解密数据流中读取解密数据
                Dim byteBuffer() As Byte = {}
                Dim intSize As Integer = 0
                Dim intBufferSize As Integer = 1024
                Dim bDes(intBufferSize - 1) As Byte
                Dim intRead As Integer = 0
                Dim i As Integer = 0
                While True
                    intRead = objCryptoStream.Read(bDes, 0, bDes.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    ReDim Preserve byteBuffer(intSize + intRead)
                    For i = 0 To intRead - 1 Step 1
                        byteBuffer(intSize + i) = bDes(i)
                    Next
                    intSize += intRead
                End While

                '获取解密后的数据
                strData = System.Text.Encoding.Unicode.GetString(byteBuffer)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objMemoryStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)

            doDecryptString = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objMemoryStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)
            Exit Function

        End Function











        '----------------------------------------------------------------
        ' 根据指定日期获取所在星期的开始日期=星期日、结束日期=星期六
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDay               ：当前日期
        '     objWeekStart         ：(返回)星期日的日期
        '     objWeekEnd           ：(返回)星期六的日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime, _
            ByRef objWeekEnd As System.DateTime) As Boolean

            getWeekStartAndEndDay = False
            strErrMsg = ""

            Try
                Dim intAdd As Integer
                Select Case objDay.DayOfWeek()
                    Case DayOfWeek.Sunday
                        intAdd = 0
                    Case DayOfWeek.Monday
                        intAdd = -1
                    Case DayOfWeek.Tuesday
                        intAdd = -2
                    Case DayOfWeek.Wednesday
                        intAdd = -3
                    Case DayOfWeek.Thursday
                        intAdd = -4
                    Case DayOfWeek.Friday
                        intAdd = -5
                    Case DayOfWeek.Saturday
                        intAdd = -6
                End Select

                '获取星期日的天
                objWeekStart = objDay.AddDays(intAdd)

                '获取星期六的天
                objWeekEnd = objWeekStart.AddDays(6)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定日期获取所在星期的开始日期=星期日、结束日期=星期六
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDay               ：当前日期
        '     objWeekStart         ：(返回)星期日的日期
        '     objWeekEnd           ：(返回)星期六的日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime, _
            ByRef objWeekEnd As System.DateTime, _
            ByVal SaturdaytoFriday As String) As Boolean

            getWeekStartAndEndDay = False
            strErrMsg = ""

            Try
                Dim intAdd As Integer
                Select Case objDay.DayOfWeek()
                    Case DayOfWeek.Sunday
                        intAdd = -1
                    Case DayOfWeek.Monday
                        intAdd = -2
                    Case DayOfWeek.Tuesday
                        intAdd = -3
                    Case DayOfWeek.Wednesday
                        intAdd = -4
                    Case DayOfWeek.Thursday
                        intAdd = -5
                    Case DayOfWeek.Friday
                        intAdd = -6
                    Case DayOfWeek.Saturday
                        intAdd = 0
                End Select

                '获取星期六的天
                objWeekStart = objDay.AddDays(intAdd)

                '获取星期五的天
                objWeekEnd = objWeekStart.AddDays(6)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定日期获取上周的开始日期
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDay               ：当前日期
        '     objWeekStart         ：(返回)上周的开始日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime) As Boolean

            getWeekStartAndEndDay = False
            strErrMsg = ""

            Try
                '获取上周的开始日期
                objWeekStart = objDay.AddDays(-7).AddDays(1)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定日期获取下一周的开始日期，结束日期
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDay               ：当前日期
        '     objWeekStart         ：(返回)上周的开始日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNextWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime, _
            ByRef objWeekEnd As System.DateTime) As Boolean

            getNextWeekStartAndEndDay = False
            strErrMsg = ""
            Dim objDayTemp As System.DateTime

            Try
                objDayTemp = objDay.AddDays(7)

                Dim intAdd As Integer
                Select Case objDayTemp.DayOfWeek()
                    Case DayOfWeek.Sunday
                        intAdd = 0
                    Case DayOfWeek.Monday
                        intAdd = -1
                    Case DayOfWeek.Tuesday
                        intAdd = -2
                    Case DayOfWeek.Wednesday
                        intAdd = -3
                    Case DayOfWeek.Thursday
                        intAdd = -4
                    Case DayOfWeek.Friday
                        intAdd = -5
                    Case DayOfWeek.Saturday
                        intAdd = -6
                End Select

                '获取星期日的天
                objWeekStart = objDayTemp.AddDays(intAdd)

                '获取星期六的天
                objWeekEnd = objWeekStart.AddDays(6)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNextWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定日期获取上一周的开始日期，结束日期
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDay               ：当前日期
        '     objWeekStart         ：(返回)上周的开始日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLastWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime, _
            ByRef objWeekEnd As System.DateTime) As Boolean

            getLastWeekStartAndEndDay = False
            strErrMsg = ""
            Dim objDayTemp As System.DateTime

            Try
                objDayTemp = objDay.AddDays(-7)

                Dim intAdd As Integer
                Select Case objDayTemp.DayOfWeek()
                    Case DayOfWeek.Sunday
                        intAdd = 0
                    Case DayOfWeek.Monday
                        intAdd = -1
                    Case DayOfWeek.Tuesday
                        intAdd = -2
                    Case DayOfWeek.Wednesday
                        intAdd = -3
                    Case DayOfWeek.Thursday
                        intAdd = -4
                    Case DayOfWeek.Friday
                        intAdd = -5
                    Case DayOfWeek.Saturday
                        intAdd = -6
                End Select

                '获取星期日的天
                objWeekStart = objDayTemp.AddDays(intAdd)

                '获取星期六的天
                objWeekEnd = objWeekStart.AddDays(6)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getLastWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据数字获取对应的中文月份名称
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intDay               ：当前对应月份数字
        '     strMonth             ：(返回)中文月份名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getChineseMonth( _
            ByRef strErrMsg As String, _
            ByVal intDay As Integer, _
            ByRef strMonth As String) As Boolean

            getChineseMonth = False
            strMonth = ""
            strErrMsg = ""

            Try
                Select Case intDay
                    Case 1
                        strMonth = "一月"
                    Case 2
                        strMonth = "二月"
                    Case 3
                        strMonth = "三月"
                    Case 4
                        strMonth = "四月"
                    Case 5
                        strMonth = "五月"
                    Case 6
                        strMonth = "六月"
                    Case 7
                        strMonth = "七月"
                    Case 8
                        strMonth = "八月"
                    Case 9
                        strMonth = "九月"
                    Case 10
                        strMonth = "十月"
                    Case 11
                        strMonth = "十一月"
                    Case 12
                        strMonth = "十二月"
                End Select
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getChineseMonth = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定日期获取上周的开始日期
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDay               ：当前日期
        '     objWeekStart         ：(返回)上周的开始日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getChineseWeek( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef strWeekName As String) As Boolean

            getChineseWeek = False
            strWeekName = ""
            strErrMsg = ""

            Try
                Select Case objDay.DayOfWeek
                    Case DayOfWeek.Sunday
                        strWeekName = "星期日"
                    Case DayOfWeek.Monday
                        strWeekName = "星期一"
                    Case DayOfWeek.Tuesday
                        strWeekName = "星期二"
                    Case DayOfWeek.Wednesday
                        strWeekName = "星期三"
                    Case DayOfWeek.Thursday
                        strWeekName = "星期四"
                    Case DayOfWeek.Friday
                        strWeekName = "星期五"
                    Case DayOfWeek.Saturday
                        strWeekName = "星期六"
                End Select
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getChineseWeek = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定日期获取所在月份的开始日期、结束日期
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDay               ：当前日期
        '     objMonthStart        ：(返回)月初日期
        '     objMonthEnd          ：(返回)月末日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMonthStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objMonthStart As System.DateTime, _
            ByRef objMonthEnd As System.DateTime) As Boolean

            getMonthStartAndEndDay = False
            strErrMsg = ""

            Try
                '获取月初日期
                objMonthStart = CType(Format(objDay, "yyyy-MM") + "-01", System.DateTime)

                '获取月末日期
                objMonthEnd = objMonthStart.AddMonths(1).AddDays(-1)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getMonthStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定日期获取所在日期前一月的开始日期
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDay               ：当前日期
        '     objMonthStart        ：(返回)开始日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMonthStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objMonthStart As System.DateTime) As Boolean

            getMonthStartAndEndDay = False
            strErrMsg = ""

            Try
                '获取开始日期
                objMonthStart = objDay.AddMonths(1).AddDays(1)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getMonthStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function










        '****************************************************************
        ' 下列代码为固定长度分级代码处理例程
        '****************************************************************
        '----------------------------------------------------------------
        ' 根据选定代码获取相应的新代码的上级代码值与新代码的长度
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intFJCDSM            ：代码分级长度描述数组
        '     strFillChar          ：代码长度不足时的填充字符
        '     strXZDM              ：当前选定代码
        '     blnIsTJ              ：=true新代码与当前选定代码同级,=false新代码为当前选定代码下级
        '     strSJDMALL           ：新代码的上级代码值的全代码值
        '     strSJDM              ：新代码的上级代码值(只有指定级别的长度，顶层可为空)
        '     intDMCD              ：新代码的长度
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewDMParamtersFixLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strFillChar As String, _
            ByVal strXZDM As String, _
            ByVal blnIsTJ As Boolean, _
            ByRef strSJDMALL As String, _
            ByRef strSJDM As String, _
            ByRef intDMCD As Integer) As Boolean

            Dim intTotalLen As Integer
            Dim intLen As Integer
            Dim i As Integer

            getNewDMParamtersFixLen = False
            strSJDMALL = ""
            strErrMsg = ""
            strSJDM = ""
            intDMCD = 0

            Try
                '检查
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If strFillChar Is Nothing Then strFillChar = ""
                strFillChar = strFillChar.Trim
                If strFillChar = "" Then
                    strErrMsg = "错误：未指定填充字符！"
                    GoTo errProc
                End If
                If intFJCDSM Is Nothing Then
                    strErrMsg = "错误：未指定代码的分级描述信息！"
                    GoTo errProc
                End If

                '获取代码分级信息
                Dim intCount As Integer = intFJCDSM.Length
                If intCount < 1 Then
                    strErrMsg = "错误：未指定代码分级情况！"
                    GoTo errProc
                End If
                intTotalLen = intFJCDSM(intCount - 1)
                If intCount = 1 Then
                    '只有一级代码
                    strSJDM = ""
                    intDMCD = intFJCDSM(0)
                    Exit Try
                End If

                '获取代码长度
                Dim intXZDMCD As Integer = Me.getStringLength(strXZDM)
                If intXZDMCD <= 0 Then
                    '只能是顶级代码！
                    strSJDM = ""
                    intDMCD = intFJCDSM(0)
                    Exit Try
                End If

                '代码为定长，长度校验
                If intXZDMCD <> intTotalLen Then
                    strErrMsg = "错误：[" + strXZDM + "]是无效的代码！"
                    GoTo errProc
                End If

                '确定选定代码级别
                Dim strFillStr As String
                Dim strTCM As String
                Dim strJBM As String
                Dim j As Integer
                For i = 0 To intCount - 1 Step 1
                    '是最后一级
                    If i = intCount - 1 Then
                        If blnIsTJ = False Then
                            strErrMsg = "错误：已经是最后一级，不能有下级！"
                            GoTo errProc
                        Else
                            strSJDM = strXZDM.Substring(0, intFJCDSM(i - 1))
                            intDMCD = intFJCDSM(i)
                            Exit Try
                        End If
                    End If

                    '级别码
                    strJBM = strXZDM.Substring(0, intFJCDSM(i))
                    '填充码
                    strTCM = strXZDM.Substring(intFJCDSM(i))
                    '是填充?
                    intLen = intTotalLen - intFJCDSM(i)
                    strFillStr = ""
                    For j = 0 To intLen - 1 Step 1
                        strFillStr += strFillChar
                    Next
                    If strFillStr <> "" Then
                        If strTCM = strFillStr Then
                            If blnIsTJ = False Then
                                strSJDM = strJBM
                                intDMCD = intFJCDSM(i + 1)
                                Exit Try
                            Else
                                If i = 0 Then
                                    strSJDM = ""
                                    intDMCD = intFJCDSM(0)
                                    Exit Try
                                Else
                                    strSJDM = strXZDM.Substring(0, intFJCDSM(i - 1))
                                    intDMCD = intFJCDSM(i)
                                    Exit Try
                                End If
                            End If
                        End If
                    End If
                Next

                '错误
                strErrMsg = "错误：无法确定上级代码！"
                GoTo errProc

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '填充上级代码
            Try
                If strSJDM <> "" Then
                    intLen = Me.getStringLength(strSJDM)
                    intLen = intTotalLen - intLen
                    strSJDMALL = strSJDM
                    For i = 0 To intLen - 1 Step 1
                        strSJDMALL += strFillChar
                    Next
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNewDMParamtersFixLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 检查代码长度是否正确？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intFJCDSM            ：代码分级长度描述数组
        '     strXZDM              ：当前选定代码
        '     blnIs                ：返回是否正确
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doValidLengthFixLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strXZDM As String, _
            ByRef blnIs As Boolean) As Boolean

            doValidLengthFixLen = False
            strErrMsg = "'"
            blnIs = False

            Try
                '检查
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If intFJCDSM Is Nothing Then
                    strErrMsg = "错误：未指定代码的分级描述信息！"
                    GoTo errProc
                End If

                '获取代码长度
                Dim intXZDMCD As Integer = Me.getStringLength(strXZDM)
                Dim intCount As Integer = intFJCDSM.Length
                Dim intTotalLen As Integer = intFJCDSM(intCount - 1)

                '代码为定长，长度校验
                If intXZDMCD <> intTotalLen Then
                    blnIs = False
                Else
                    blnIs = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doValidLengthFixLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 判断选定代码是否为明细码。如果不是明细，则返回不带填充码的代码值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intFJCDSM            ：代码分级长度描述数组
        '     strFillChar          ：代码长度不足时的填充字符
        '     strXZDM              ：当前选定代码
        '     blnIs                ：是否明细代码
        '     strDMJZ              ：返回不带填充码的代码值
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function isDetailFixLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strFillChar As String, _
            ByVal strXZDM As String, _
            ByRef blnIs As Boolean, _
            ByRef strDMJZ As String) As Boolean

            Dim intTotalLen As Integer
            Dim intLen As Integer
            Dim i As Integer

            isDetailFixLen = False
            strErrMsg = ""
            blnIs = True
            strDMJZ = ""

            Try
                '检查
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If strXZDM = "" Then
                    Exit Try
                End If
                If strFillChar Is Nothing Then strFillChar = ""
                strFillChar = strFillChar.Trim
                If strFillChar = "" Then
                    strErrMsg = "错误：未指定填充字符！"
                    GoTo errProc
                End If
                If intFJCDSM Is Nothing Then
                    strErrMsg = "错误：未指定代码的分级描述信息！"
                    GoTo errProc
                End If

                '获取代码分级信息
                Dim intCount As Integer = intFJCDSM.Length
                If intCount < 1 Then
                    strErrMsg = "错误：未指定代码分级情况！"
                    GoTo errProc
                End If
                intTotalLen = intFJCDSM(intCount - 1)

                '判断代码是否为明细代码
                Dim strValue As String
                Dim strXDM As String
                Dim j As Integer
                For i = 0 To intCount - 2 Step 1
                    strXDM = strXZDM.Substring(0, intFJCDSM(i))
                    strValue = strXDM
                    intLen = intTotalLen - intFJCDSM(i)
                    For j = 0 To intLen - 1 Step 1
                        strXDM = strXDM + strFillChar
                    Next
                    If strXDM = strXZDM Then
                        '非明细代码
                        strDMJZ = strValue
                        blnIs = False
                        Exit Try
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isDetailFixLen = True
            Exit Function
errProc:
            Exit Function
        End Function




        '****************************************************************
        ' 下列代码为变长长度分级代码处理例程
        '****************************************************************
        '----------------------------------------------------------------
        ' 根据选定代码获取相应的新代码的上级代码值与新代码的长度
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intFJCDSM            ：代码分级长度描述数组
        '     strXZDM              ：当前选定代码
        '     blnIsTJ              ：=true新代码与当前选定代码同级,=false新代码为当前选定代码下级
        '     strSJDMALL           ：新代码的上级代码值的全代码值
        '     strSJDM              ：新代码的上级代码值(只有指定级别的长度，顶层可为空)
        '     intDMCD              ：新代码的长度
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewDMParamtersVarLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strXZDM As String, _
            ByVal blnIsTJ As Boolean, _
            ByRef strSJDMALL As String, _
            ByRef strSJDM As String, _
            ByRef intDMCD As Integer) As Boolean

            Dim intLen As Integer
            Dim i As Integer

            getNewDMParamtersVarLen = False
            strSJDMALL = ""
            strErrMsg = ""
            strSJDM = ""
            intDMCD = 0

            Try
                '检查
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If intFJCDSM Is Nothing Then
                    strErrMsg = "错误：未指定代码的分级描述信息！"
                    GoTo errProc
                End If

                '获取代码分级信息
                Dim intCount As Integer = intFJCDSM.Length
                If intCount < 1 Then
                    strErrMsg = "错误：未指定代码分级情况！"
                    GoTo errProc
                End If
                If intCount = 1 Then
                    '只有一级代码
                    strSJDM = ""
                    intDMCD = intFJCDSM(0)
                    Exit Try
                End If

                '获取代码长度
                Dim intXZDMCD As Integer = Me.getStringLength(strXZDM)
                If intXZDMCD <= 0 Then
                    '只能是顶级代码！
                    strSJDM = ""
                    intDMCD = intFJCDSM(0)
                    Exit Try
                End If

                '校验代码长度
                Dim blnFound As Boolean = False
                For i = 0 To intCount - 1 Step 1
                    If intXZDMCD = intFJCDSM(i) Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    strErrMsg = "错误：代码长度不正确！"
                    GoTo errProc
                End If

                '确定选定代码级别
                For i = 0 To intCount - 1 Step 1
                    If intXZDMCD = intFJCDSM(i) Then
                        '选定代码为i级
                        If blnIsTJ = True Then
                            intDMCD = intFJCDSM(i)
                            If i = 0 Then
                                strSJDM = ""
                            Else
                                strSJDM = strXZDM.Substring(0, intFJCDSM(i - 1))
                            End If
                        Else
                            If i = intCount - 1 Then
                                strErrMsg = "错误：已经是最后一级代码，不能有下级！"
                                GoTo errProc
                            Else
                                intDMCD = intFJCDSM(i + 1)
                                strSJDM = strXZDM
                            End If
                        End If
                        Exit Try
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '不用填充
            strSJDMALL = strSJDM

            getNewDMParamtersVarLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 检查代码长度是否正确？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intFJCDSM            ：代码分级长度描述数组
        '     strXZDM              ：当前选定代码
        '     blnIs                ：返回是否正确
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doValidLengthVarLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strXZDM As String, _
            ByRef blnIs As Boolean) As Boolean

            doValidLengthVarLen = False
            strErrMsg = "'"
            blnIs = False

            Try
                '检查
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If intFJCDSM Is Nothing Then
                    strErrMsg = "错误：未指定代码的分级描述信息！"
                    GoTo errProc
                End If

                '获取代码长度
                Dim intXZDMCD As Integer = Me.getStringLength(strXZDM)
                Dim intCount As Integer = intFJCDSM.Length
                Dim i As Integer

                '校验代码长度
                Dim blnFound As Boolean = False
                For i = 0 To intCount - 1 Step 1
                    If intXZDMCD = intFJCDSM(i) Then
                        blnFound = True
                        Exit For
                    End If
                Next

                '返回
                blnIs = blnFound

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doValidLengthVarLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 判断选定代码是否为明细码。如果不是明细，则返回不带填充码的代码值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intFJCDSM            ：代码分级长度描述数组
        '     strXZDM              ：当前选定代码
        '     blnIs                ：是否明细代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function isDetailVarLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strXZDM As String, _
            ByRef blnIs As Boolean) As Boolean

            Dim intLen As Integer
            Dim i As Integer

            isDetailVarLen = False
            strErrMsg = ""
            blnIs = True

            Try
                '检查
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If strXZDM = "" Then
                    Exit Try
                End If
                If intFJCDSM Is Nothing Then
                    strErrMsg = "错误：未指定代码的分级描述信息！"
                    GoTo errProc
                End If

                '获取代码分级信息
                Dim intCount As Integer = intFJCDSM.Length
                If intCount < 1 Then
                    strErrMsg = "错误：未指定代码分级情况！"
                    GoTo errProc
                End If

                '判断代码是否为明细代码
                intLen = Me.getStringLength(strXZDM)
                For i = 0 To intCount - 2 Step 1
                    If intLen = intFJCDSM(i) Then
                        blnIs = False
                        Exit Try
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isDetailVarLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '获取数字strDigital对应的汉字表示(○、一、二、...)
        Public Function getChineseNumber(ByVal strDigital As String) As String

            Dim strTemp(10) As String
            Dim intCount As Integer
            Dim i As Integer

            '初始化
            strDigital = strDigital.Trim
            getChineseNumber = strDigital

            Try
                strTemp(0) = strDigital

                '转换
                intCount = Len(strTemp(0))
                strTemp(1) = ""
                For i = 1 To intCount Step 1
                    strTemp(2) = Mid(strTemp(0), i, 1)
                    Select Case strTemp(2)
                        Case "0"
                            strTemp(3) = "○"
                        Case "1"
                            strTemp(3) = "一"
                        Case "2"
                            strTemp(3) = "二"
                        Case "3"
                            strTemp(3) = "三"
                        Case "4"
                            strTemp(3) = "四"
                        Case "5"
                            strTemp(3) = "五"
                        Case "6"
                            strTemp(3) = "六"
                        Case "7"
                            strTemp(3) = "七"
                        Case "8"
                            strTemp(3) = "八"
                        Case "9"
                            strTemp(3) = "九"
                    End Select
                    strTemp(1) = strTemp(1) + strTemp(3)
                Next

                getChineseNumber = strTemp(1)

            Catch ex As Exception
            End Try

        End Function

        '获取数字strDigital对应的汉字表示(1.)
        Public Function getChineseNumber(ByVal strErrMsg As String, ByVal strDigital As String) As Integer

            Dim strTemp As String
            Dim intCount As Integer
            Dim i As Integer

            '初始化
            strDigital = strDigital.Trim
            getChineseNumber = 0

            Try
                Select Case strDigital
                    Case "一月"
                        intCount = 1
                    Case "二月"
                        intCount = 2
                    Case "三月"
                        intCount = 3
                    Case "四月"
                        intCount = 4
                    Case "五月"
                        intCount = 5
                    Case "六月"
                        intCount = 6
                    Case "七月"
                        intCount = 7
                    Case "八月"
                        intCount = 8
                    Case "九月"
                        intCount = 9
                    Case "十月"
                        intCount = 10
                    Case "九月"
                        intCount = 11
                    Case "十月"
                        intCount = 12
                End Select

                getChineseNumber = intCount

            Catch ex As Exception
            End Try

        End Function

        '获取月份对应的汉字表示
        Public Function getChineseMonth(ByVal strDigital As String) As String

            Dim strTemp(10) As String
            Dim intValue As Integer

            '初始化
            strDigital = strDigital.Trim
            getChineseMonth = strDigital

            Try
                intValue = Me.getObjectValue(strDigital, 1)

                '转换
                Select Case intValue
                    Case 1
                        strTemp(0) = "一"
                    Case 2
                        strTemp(0) = "二"
                    Case 3
                        strTemp(0) = "三"
                    Case 4
                        strTemp(0) = "四"
                    Case 5
                        strTemp(0) = "五"
                    Case 6
                        strTemp(0) = "六"
                    Case 7
                        strTemp(0) = "七"
                    Case 8
                        strTemp(0) = "八"
                    Case 9
                        strTemp(0) = "九"
                    Case 10
                        strTemp(0) = "十"
                    Case 11
                        strTemp(0) = "十一"
                    Case 12
                        strTemp(0) = "十二"
                End Select

            Catch ex As Exception

            End Try

            getChineseMonth = strTemp(0)

        End Function

        '获取天数对应的汉字表示
        Public Function getChineseDay(ByVal strDigital As String) As String

            Dim strTemp(10) As String
            Dim intValue As Integer

            '初始化
            strDigital = Trim(strDigital)
            getChineseDay = strDigital

            Try
                intValue = Me.getObjectValue(strDigital, 1)

                '转换
                Select Case intValue
                    Case 1
                        strTemp(0) = "一"
                    Case 2
                        strTemp(0) = "二"
                    Case 3
                        strTemp(0) = "三"
                    Case 4
                        strTemp(0) = "四"
                    Case 5
                        strTemp(0) = "五"
                    Case 6
                        strTemp(0) = "六"
                    Case 7
                        strTemp(0) = "七"
                    Case 8
                        strTemp(0) = "八"
                    Case 9
                        strTemp(0) = "九"
                    Case 10
                        strTemp(0) = "十"
                    Case 11
                        strTemp(0) = "十一"
                    Case 12
                        strTemp(0) = "十二"
                    Case 13
                        strTemp(0) = "十三"
                    Case 14
                        strTemp(0) = "十四"
                    Case 15
                        strTemp(0) = "十五"
                    Case 16
                        strTemp(0) = "十六"
                    Case 17
                        strTemp(0) = "十七"
                    Case 18
                        strTemp(0) = "十八"
                    Case 19
                        strTemp(0) = "十九"
                    Case 20
                        strTemp(0) = "二十"
                    Case 21
                        strTemp(0) = "二十一"
                    Case 22
                        strTemp(0) = "二十二"
                    Case 23
                        strTemp(0) = "二十三"
                    Case 24
                        strTemp(0) = "二十四"
                    Case 25
                        strTemp(0) = "二十五"
                    Case 26
                        strTemp(0) = "二十六"
                    Case 27
                        strTemp(0) = "二十七"
                    Case 28
                        strTemp(0) = "二十八"
                    Case 29
                        strTemp(0) = "二十九"
                    Case 30
                        strTemp(0) = "三十"
                    Case 31
                        strTemp(0) = "三十一"
                End Select

                getChineseDay = strTemp(0)

            Catch ex As Exception
            End Try

        End Function

        '获取日期对应的汉字表示
        Public Function getChineseDate(ByVal objDate As System.DateTime) As String

            Dim strTemp(10) As String

            getChineseDate = ""

            Try

                strTemp(0) = objDate.Year.ToString
                If strTemp(0) = "" Then
                    Exit Try
                End If
                strTemp(1) = getChineseNumber(strTemp(0))

                strTemp(2) = objDate.Month.ToString
                strTemp(3) = getChineseMonth(strTemp(2))

                strTemp(4) = objDate.Day.ToString
                strTemp(5) = getChineseDay(strTemp(4))

                getChineseDate = strTemp(1) + "年" + strTemp(3) + "月" + strTemp(5) + "日"

            Catch ex As Exception
            End Try

        End Function



        '----------------------------------------------------------------
        ' 检查字符串搜索条件
        ' 根据SearchConfig进行设置
        '     = True ：任意位置匹配
        '     = False：从头匹配
        '----------------------------------------------------------------
        Public Function getNewSearchString(ByVal strValue As String) As String

            If strValue Is Nothing Then strValue = ""
            Try
                If strValue = "" Then
                    getNewSearchString = strValue
                Else
                    If strValue.Substring(0, 1) = "%" Then
                        getNewSearchString = strValue
                    Else
                        If SearchConfig = True Then
                            getNewSearchString = "%" + strValue
                        Else
                            getNewSearchString = strValue
                        End If
                    End If
                End If
            Catch ex As Exception
                getNewSearchString = strValue
            End Try

        End Function

    End Class 'PulicParameters

End Namespace 'Xydc.Platform.Common.Utilities
