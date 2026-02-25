<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" 
    CodeFile="frmSaleForce.aspx.cs" Inherits="Forms_frmSaleForce" Title="SAMS :: Employee Information" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;
            str = document.getElementById('<%=txtUserName.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter User Name');
                return false;
            }
            str = document.getElementById('<%=txtNICNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter NIC No');
                return false;
            }

            str = document.getElementById('<%=txtMobileNo.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Mobile No');
                return false;
            }
            str = document.getElementById('<%=txtAddress2.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Address');
                return false;
            }
            return true;
        }
    </script>    
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <div id="right_data">
        <div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                        Width="31px" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="btnSalaryStructure" Style="float: none;" OnClick="btnSalaryStructure_Click"
                                    runat="server">Salary Structure</asp:LinkButton>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                    DropShadow="true" PopupControlID="pnlSalaryStructure" TargetControlID="btnSalaryStructure">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlSalaryStructure" runat="server" Style="display: none; background-color: White;"
                                    ScrollBars="Auto" Height="500px" Width="100%">
                                    <table width="100%" style="padding-left: 50px;">
                                        <tbody>
                                            <tr>
                                                <td width="10%">
                                                    Basic Salary
                                                </td>
                                                <td width="25%" valign="middle" align="center">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtBasicSalary" runat="server"
                                                        ControlToValidate="txtBasicSalary" ErrorMessage="Enter Basic Pay" ValidationGroup="grpSalaryStructure"></asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtBasicSalary" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnSalaryStructureID" runat="server" />
                                                </td>
                                                <td width="10%">
                                                    Shift
                                                </td>
                                                <td width="20%">
                                                    <asp:DropDownList ID="ddlShift" runat="server" OnSelectedIndexChanged="ddlSalaryTemplate_Change"
                                                        Width="200px" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="10%">
                                                    <label>
                                                        Salary Template</label>
                                                </td>
                                                <td width="30%">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_ddlSalaryTemplate" runat="server"
                                                        InitialValue="0" ControlToValidate="ddlSalaryTemplate" ErrorMessage="Select Salary Template"
                                                        ValidationGroup="grpSalaryStructure"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlSalaryTemplate" runat="server" OnSelectedIndexChanged="ddlSalaryTemplate_Change"
                                                        Width="200px" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h2>
                                                        Allowancs</h2>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptAllAllowances" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td width="20%">
                                                                    <label>
                                                                        <%#Eval("AllowanceDescription")%>
                                                                    </label>
                                                                </td>
                                                                <td width="30%">
                                                                    <asp:CheckBox ID="chkAllowancePercentage" runat="server" Width="20px" Checked='<%#Eval("RatioType").ToString() == "1" ? true:false %>' /><label>
                                                                        Percentage</label>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAllowanceAmount" runat="server"
                                                                        ControlToValidate="txtAllowanceAmount" ErrorMessage="Enter Allowance" ValidationGroup="grpSalaryStructure"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtAllowanceAmount" Text='<%# Eval("AllowanceRatio") %>' runat="server"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnAllAllownaceID" Value='<%# Eval("AllowanceID") %>' runat="server">
                                                                    </asp:HiddenField>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h2>
                                                        Deductions</h2>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Repeater ID="rptAllDeduction" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td width="20%">
                                                                    <label>
                                                                        <%#Eval("DeductionDescription")%>
                                                                    </label>
                                                                </td>
                                                                <td width="30%">
                                                                    <asp:CheckBox ID="chkDeductionPercentage" runat="server" Width="20px" Checked='<%#Eval("RatioType").ToString() == "1" ? true:false %>' /><label>
                                                                        Percentage</label>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAllowanceAmount" runat="server"
                                                                        ControlToValidate="txtDeductionAmount" ErrorMessage="Enter Deductions" ValidationGroup="grpSalaryStructure"></asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtDeductionAmount" Text='<%# Eval("DeductionRatio") %>' runat="server"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnAllDeductionID" Value='<%# Eval("DeductionID") %>' runat="server">
                                                                    </asp:HiddenField>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:TextBox ID="txtComments" TextMode="MultiLine" Width="90%" Text='<%# Eval("Comments") %>'
                                                                        placeholder="Enter Comments" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%if (Session["EditEmployeeID"] != null)
                                                      {%>
                                                    <asp:LinkButton ID="btnSaveSalaryStructure" ValidationGroup="grpSalaryStructure"
                                                        runat="server" OnClick="btnSaveSalaryStructure_Click"><img src="../images/btn-update.png" /></asp:LinkButton>
                                                    <%}
                                                      else
                                                      { %>
                                                    <asp:LinkButton ID="btnUpdateSalaryStructure" ValidationGroup="grpSalaryStructure"
                                                        runat="server" OnClick="btnSaveSalaryStructure_Click"><img src="../images/btn-save.png"/></asp:LinkButton>
                                                    <%} %>
                                                </td>
                                                <%--                         <td>
                          <asp:LinkButton ID="btnDiscardSalaryStructure" runat="server" OnClick="btnDiscardSalaryStructure_Click">Discard</asp:LinkButton>
                         </td>--%>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td style="width: 143px" align="left">
                                            </td>
                                            <td style="width: 175px">
                                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="lbldesignationID" runat="server" Width="94px" Text="Base Location"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 15px">
                                                <asp:DropDownList ID="ddDistributorId" runat="server" Width="202px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 15px">
                                                &nbsp; &nbsp;
                                            </td>
                                            <td style="width: 143px; height: 12px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="79px" Text="Designation" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 12px">
                                                <asp:DropDownList ID="ddDesignation" runat="server" Width="202px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label16" runat="server" Width="94px" Text="Department" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 15px">
                                                <asp:DropDownList ID="DrpDepartment" runat="server" Width="202px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 15px">
                                                &nbsp; &nbsp;
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label15" runat="server" Width="122px" Text="Reporting to Desig." CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpReportingTo" runat="server" Width="202px" CssClass="DropList"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DrpReportingTo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label17" runat="server" Width="94px" Text="Gender" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 15px">
                                                <asp:DropDownList ID="DrpGender" runat="server" Width="202px" CssClass="DropList">
                                                    <asp:ListItem Value="0">Male</asp:ListItem>
                                                    <asp:ListItem Value="1">Female</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 15px">
                                                &nbsp; &nbsp;
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label18" runat="server" Width="122px" Text="Reporting to Emp." CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpReportingToExmployee" runat="server" Width="202px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label19" runat="server" Width="94px" Text="Marital Staus" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 15px">
                                                <asp:DropDownList ID="DrpMaritalStatus" runat="server" Width="202px" CssClass="DropList">
                                                    <asp:ListItem Value="False">Single</asp:ListItem>
                                                    <asp:ListItem Value="True">Married</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 15px">
                                                &nbsp; &nbsp;
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label20" runat="server" Width="122px" Text="Salary Charged to" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DrpSalaryChargedTo" runat="server" Width="202px" CssClass="DropList">
                                                    <asp:ListItem Value="0">Office</asp:ListItem>
                                                    <asp:ListItem Value="1">Principal</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label27" runat="server" Width="94px" Text="Employee Type"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 175px; height: 15px">
                                                <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="202px">
                                                    <asp:ListItem Value="0">Permanent</asp:ListItem>
                                                    <asp:ListItem Value="1">Contractual</asp:ListItem>
                                                    <asp:ListItem Value="2">Dailywages</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1px; height: 15px">
                                                &nbsp; &nbsp;
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label28" runat="server" Width="122px" Text="Date Of Joining"></asp:Label>
                                                </strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJoiningDate" runat="server" Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="imgJoiningDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                                <cc1:CalendarExtender ID="ceJoiningDate" runat="server" EnableViewState="False"
                                                    Format="dd-MMM-yyyy" PopupButtonID="imgJoiningDate" TargetControlID="txtJoiningDate">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label29" runat="server" Width="96px" Text="Probation From" ></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="height: 15px">
                                                <asp:TextBox ID="txtProbationFrom" runat="server" Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="imgProbationFrom" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                                <cc1:CalendarExtender ID="ceProbationFrom" runat="server" EnableViewState="False"
                                                    Format="dd-MMM-yyyy" PopupButtonID="imgProbationFrom" TargetControlID="txtProbationFrom">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td style="width: 1px; height: 12px">
                                            </td>
                                            <td style="width: 143px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label30" runat="server" Width="82px" Text="Probation To"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 175px">
                                                <asp:TextBox ID="txtProbationTo" runat="server" Width="150px"></asp:TextBox>
                                                <asp:ImageButton ID="imgProbationTo" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                                <cc1:CalendarExtender ID="ceProbationTo" runat="server" EnableViewState="False"
                                                    Format="dd-MMM-yyyy" PopupButtonID="imgProbationTo" TargetControlID="txtProbationTo">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 15px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="96px" Text="Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 15px">
                                                <asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 12px">
                                            </td>
                                            <td style="width: 143px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="82px" Text="Father Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px">
                                                <asp:TextBox ID="txtFatherName" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="122px" Text="Husband Name" CssClass="lblbox"
                                                        Height="16px"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtHusbandName" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 1px; height: 22px">
                                            </td>
                                            <td style="width: 143px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="112px" Text="Religion" CssClass="lblbox"
                                                        Height="16px"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px">
                                                <asp:TextBox ID="txtReligion" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px; height: 22px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblMobileNo" runat="server" Width="96px" Text="Mobile No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px; height: 22px">
                                                <asp:TextBox ID="txtMobileNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="height: 12px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="79px" Text="N.I.C No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 12px">
                                                <asp:TextBox ID="txtNICNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 22px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblPhNo" runat="server" Width="90px" Text="Phone No" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 22px">
                                                <asp:TextBox ID="txtPhoneNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label7" runat="server" Width="127px" Text="No of Dependents" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNoOfDependents" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label14" runat="server" Width="82px" Text="Reference" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 175px">
                                                <asp:TextBox ID="txtReferance" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 143px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblEmail" runat="server" Width="87px" Text="Email Address" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblAddress1" runat="server" Width="119px" Text="Present Address "
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtAddress1" runat="server" Width="99.5%" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblAddress2" runat="server" Width="125px" Text="Permanent Address "
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtAddress2" runat="server" Width="99.5%" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Panel ID="Panel13" runat="server" GroupingText="Emergency POC Detail" Style="margin-top: 0px;
                                                    margin-bottom: 0px;">
                                                    <table>
                                                        <tr>
                                                            <td style="width: 143px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label21" runat="server" Width="129px" Text="POC Name" CssClass="lblbox"
                                                                        Height="16px"></asp:Label></strong>
                                                            </td>
                                                            <td style="width: 175px">
                                                                <asp:TextBox ID="txtEmergencyPersonName" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1px">
                                                            </td>
                                                            <td style="width: 143px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label22" runat="server" Width="126px" Text="POC Contact No" CssClass="lblbox"
                                                                        Height="16px"></asp:Label></strong>
                                                            </td>
                                                            <td colspan="4">
                                                                <asp:TextBox ID="txtEmergencyContactNo" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Panel ID="Panel12" runat="server" GroupingText="Bank Info" Style="margin-top: 0px;
                                                    margin-bottom: 0px;">
                                                    <table>
                                                        <tr>
                                                            <td align="left" style="width: 143px">
                                                                <strong>
                                                                    <asp:Label ID="Label23" runat="server" CssClass="lblbox" Height="16px" Text="Payment Mode"
                                                                        Width="111px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 175px">
                                                                <asp:DropDownList ID="DrpPaymentMode" runat="server" CssClass="DropList" Width="202px">
                                                                    <asp:ListItem Value="0">Cash</asp:ListItem>
                                                                    <asp:ListItem Value="1">Bank</asp:ListItem>
                                                                    <asp:ListItem Value="2">Cheque</asp:ListItem>
                                                                    <asp:ListItem Value="3">Cash & Cheque</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 1px">
                                                            </td>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label24" runat="server" CssClass="lblbox" Text="Bank Name" Width="128px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 143px">
                                                                <strong>
                                                                    <asp:Label ID="Label25" runat="server" CssClass="lblbox" Text="Account Title" Width="112px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 175px">
                                                                <asp:TextBox ID="txtAccountTitle" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1px">
                                                            </td>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label26" runat="server" CssClass="lblbox" Text="Account No" Width="124px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Panel ID="POC" runat="server" GroupingText="Last Job Detail" CssClass="clear;"
                                                    Style="margin-top: 0px; margin-bottom: 0px;">
                                                    <table>
                                                        <tr>
                                                            <td align="left" style="width: 143px">
                                                                <strong>
                                                                    <asp:Label ID="Label8" runat="server" CssClass="lblbox" Height="16px" Text="Last Education"
                                                                        Width="111px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 175px">
                                                                <asp:TextBox ID="txtLastEducation" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1px">
                                                            </td>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label9" runat="server" CssClass="lblbox" Text="Last Employeer Info"
                                                                        Width="128px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLastEmployeeInfo" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 143px">
                                                                <strong>
                                                                    <asp:Label ID="Label10" runat="server" CssClass="lblbox" Text="Last Salary Drawn"
                                                                        Width="112px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 175px">
                                                                <asp:TextBox ID="txtLastSalaryDrawn" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom"
                                                                    TargetControlID="txtLastSalaryDrawn" ValidChars="01234567890.">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                            <td style="width: 1px">
                                                            </td>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label11" runat="server" CssClass="lblbox" Text="Last Designation"
                                                                        Width="124px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLastDesignation" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 143px">
                                                                <strong>
                                                                    <asp:Label ID="Label12" runat="server" CssClass="lblbox" Height="16px" Text="Reason of Resignation"
                                                                        Width="139px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td style="width: 175px">
                                                                <asp:TextBox ID="txtReasionOfResignation" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1px">
                                                            </td>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label13" runat="server" CssClass="lblbox" Text="Last Contact No" Width="126px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLastContactNo" runat="server" CssClass="txtBox " Width="200px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 143px; height: 21px">
                                            </td>
                                            <td style="width: 175px; height: 21px">
                                                <asp:Button ID="btnSave" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnSave_Click"
                                                    Text="Save" ValidationGroup="vg" Width="82px" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnCancel_Click"
                                                    Text="Cancel" Width="73px" />
                                            </td>
                                            <td style="width: 1px; height: 21px">
                                            </td>
                                            <td style="height: 21px">
                                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Text="IsActive" Width="93px" />
                                            </td>
                                            <td style="height: 21px">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Vertical" Width="100%"
                        BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                        <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="gridRow2"
                            BorderColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White"
                            OnRowCommand="Grid_users_RowCommand" PageSize="15">
                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                PreviousPageText="Previous"></PagerSettings>
                            <Columns>
                                <asp:BoundField DataField="USER_ID" HeaderText="User Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_CODE" HeaderText="Code">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_NAME" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NIC_NO" HeaderText="NIC No">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PHONE" HeaderText="Phone No">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MOBILE" HeaderText="Mobile">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL" HeaderText="Email">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS1" HeaderText="Present Address">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ADDRESS2" HeaderText="Permanent Address">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel">
                                    </ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SLASH_DESC" HeaderText="Designation">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_TYPE_ID" HeaderText="USERTYPE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="COMPANY_ID" HeaderText="COMPANY_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FATHER_NAME" HeaderText="FATHER_NAME">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HUSBAND_NAME" HeaderText="HUSBAND_NAME">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="RELIGION" HeaderText="RELIGION">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NO_OF_DEPENDENTS" HeaderText="NO_OF_DEPENDENTS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LAST_EDUCATION" HeaderText="LAST_EDUCATION">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LAST_EMPLOYEE_INFO" HeaderText="LAST_EMPLOYEE_INFO">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LAST_SALARY_DRAWN" HeaderText="LAST_SALARY_DRAWN">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LAST_DESIGNATION" HeaderText="LAST_DESIGNATION">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LAST_REASON_OF_RESIGNATION" HeaderText="LAST_REASON_OF_RESIGNATION">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LAST_CONTACT_NO" HeaderText="LAST_CONTACT_NO">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REFERANCE" HeaderText="REFERANCE">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REPORTING_TO" HeaderText="REPORTING_TO">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DEPARTMENT_ID" HeaderText="DEPARTMENT_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REMARKS" HeaderText="REMARKS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Gender" HeaderText="Gender">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMERGENCY_POC_NAME" HeaderText="EMERGENCY_POC_NAME">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMERGENCY_POC_NO" HeaderText="EMERGENCY_POC_NO">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="MaritalStatus" HeaderText="MaritalStatus">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PAYMENT_MODE" HeaderText="PAYMENT_MODE">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BANK_NAME" HeaderText="BANK_NAME">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BANK_ACCOUNT_TITLE" HeaderText="BANK_ACCOUNT_TITLE">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BANK_ACCOUNT_NO" HeaderText="BANK_ACCOUNT_NO">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SALARY_CHARGED_TO" HeaderText="SALARY_CHARGED_TO">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REPORTING_TO_EMPLOYEE" HeaderText="REPORTING_TO_EMPLOYEE">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SALARY_STRUCTURE_ID" HeaderText="SALARY_STRUCTURE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TEMPLATE_ID" HeaderText="TEMPLATE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SHIFT_ID" HeaderText="SHIFT_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeType" HeaderText="EmployeeType">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeJoiningDate" HeaderText="EmployeeJoiningDate">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeProbationFrom" HeaderText="EmployeeProbationFrom">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeProbationTo" HeaderText="EmployeeProbationTo">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                        Width="35px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                            </HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
        </div>
    </div>
</asp:Content>