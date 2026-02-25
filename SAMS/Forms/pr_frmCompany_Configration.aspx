<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmCompany_Configration.aspx.cs" Inherits="Forms_pr_frmCompany_Configration"
    Title="Company Configration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">

        function HidePopupPanel(source, args) {
            //find the panel associated with the extender
            objPanel = document.getElementById(source._PopupControlID);

            //check the panel exists
            if (objPanel) {
                //set the display attribute, so it remains hidden on postback
                objPanel.style.display = 'none';
            }
        }

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
    <style type="text/css">
        .cmp input
        {
            width: 90%;
        }
        .cmp select
        {
            width: 90%;
        }
        .list input
        {
            width: 20%;
        }
        .list
        {
            width: 90%;
        }
    </style>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                <table width="100%">
                    <tbody>
                        <tr>
                            <td>
                                <table width="100%" class="cmp">
                                    <tbody>
                                        <tr>
                                            <td width="10%">
                                                <label id="lbl" runat="server" style="cursor: pointer;">
                                                </label>
                                            </td>
                                            <td width="10%">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 345px">
                                <cc1:TabContainer ID="tbUserAssignment" runat="server" Width="1000px" Height="400px"
                                    AutoPostBack="True" ActiveTabIndex="0">
                                    <cc1:TabPanel runat="server" TabIndex="0" ID="TabPanel1">
                                        <HeaderTemplate>
                                            Departments
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlDepartmentContent" runat="server">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Department Name
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="txtDepartmentName_RequiredFieldValidator" runat="server"
                                                                                    ControlToValidate="txtDepartmentName" ErrorMessage="Enter Department Name" ValidationGroup="grpDepartment"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtDepartmentName" runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnDepartmentID" runat="server" />
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <label>
                                                                                    Parent Department
                                                                                </label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlParentDepartment" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="btnSaveDepartment" runat="server" OnClick="btnSaveDepartment_Click"
                                                                                        ValidationGroup="grpDepartment">
                                                                                        <asp:Image runat="server" ImageUrl="../images/btn-save.png" ID="imgDepartment"></asp:Image>
                                                                                    </asp:LinkButton>
                                                                                    <asp:LinkButton ID="btnDiscardDepartment" runat="server" OnClick="btnDiscardDepartment_Click">Discard</asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlDepartmentGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <input type="text" id="txtDepartment" name="n" style="width: 500px;" placeholder="Enter Text To Search " />
                                                                <asp:LinkButton ID="btnShowpnlDepartmentContent" OnClick="btnShowpnlDepartmentContent_Click"
                                                                    runat="server" Style="float: right;">Add New Department</asp:LinkButton>
                                                                <br />
                                                                <asp:Repeater ID="rptDepartments" runat="server" OnItemCommand="rptDepartments_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tbody id="tblDepartment">
                                                                                <tr class="tblheading">
                                                                                    <td>
                                                                                        Department Name
                                                                                    </td>
                                                                                    <td>
                                                                                        Parent Department
                                                                                    </td>
                                                                                    <td>
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("DepartmentID")%>'
                                                                                    CommandName="editdepartment" runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("DepartmentName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton5" CommandArgument='<%# Eval("DepartmentID")%>' CommandName="editdepartment"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("pDepartmentName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDel" CommandArgument='<%# Eval("DepartmentID")%>' CommandName="deldepartment"
                                                                                    runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("DepartmentID")%>'
                                                                                    CommandName="editdepartment" runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("DepartmentName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton5" CommandArgument='<%# Eval("DepartmentID")%>' CommandName="editdepartment"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("pDepartmentName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDel" CommandArgument='<%# Eval("DepartmentID")%>' CommandName="deldepartment"
                                                                                    runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" TabIndex="1" ID="TabPanel2">
                                        <HeaderTemplate>
                                            Designations
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlDesignationContent" runat="server">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Designation Name
                                                                                    <asp:HiddenField ID="hdnDesignationID" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtDesignationName" ErrorMessage="Enter Designation Name"
                                                                                    ControlToValidate="txtDesignationName" ValidationGroup="grpDesignation" runat="server"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtDesignationName" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnsaveDesignation" ValidationGroup="grpDesignation" runat="server"
                                                                                    OnClick="btnsaveDesignation_Click">
                                                                                    <asp:Image ID="imgDesignation" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDiscardDesignation" runat="server" OnClick="btnDiscardDesignation_Click">Discard</asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlDesignationGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <input type="text" id="txtDesignation" name="n" style="width: 500px;" placeholder="Enter Text To Search " />
                                                                <asp:LinkButton ID="btnShowpnlDesignationContent" OnClick="btnShowpnlDesignationContent_Click"
                                                                    runat="server" Style="float: right;">Add New Designation</asp:LinkButton>
                                                                <br />
                                                                <asp:Repeater ID="rptDesignation" runat="server" OnItemCommand="rptDesignation_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tbody id="tblDesignation">
                                                                                <tr class="tblheading">
                                                                                    <td>
                                                                                        Designation Name
                                                                                    </td>
                                                                                    <td>
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("DesignationID")%>'
                                                                                    CommandName="editdesignation" runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("DesignationName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelDesignation" CommandArgument='<%# Eval("DesignationID")%>'
                                                                                    CommandName="deldesignation" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("DesignationID")%>'
                                                                                    CommandName="editdesignation" runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("DesignationName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelDesignation" CommandArgument='<%# Eval("DesignationID")%>'
                                                                                    CommandName="deldesignation" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="2" ID="TabPanel3">
                                        <HeaderTemplate>
                                            Allowances
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlAllowancesContent" runat="server">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Allowance Description
                                                                                    <asp:HiddenField ID="hdnAllowanceID" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAllowanceDescription" ErrorMessage="Enter Allowance Description"
                                                                                    ControlToValidate="txtAllowanceDescription" ValidationGroup="grpAllowance" runat="server"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtAllowanceDescription" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RadioButton ID="rdbValue" Width="25px" GroupName="RatioType" runat="server" />
                                                                                <label>
                                                                                    Value</label>
                                                                                <asp:RadioButton ID="rdbPercentage" Width="25px" GroupName="RatioType" runat="server" />
                                                                                <label>
                                                                                    Percentage</label>
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Ratio
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:TextBox ID="txtAllowanceRatio" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnSaveAllowance" ValidationGroup="grpAllowance" runat="server"
                                                                                    OnClick="btnSaveAllowance_Click">
                                                                                    <asp:Image ID="imgAllowance" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDiscardAllowance" runat="server" OnClick="btnDiscardAllowance_Click">Discard</asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlAllowancesGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <input type="text" id="txtAllowance" name="n" style="width: 500px;" placeholder="Enter Text To Search " />
                                                                <asp:LinkButton ID="btnShowAllowancesContent" OnClick="btnShowAllowancesContent_Click"
                                                                    runat="server">Add New Allowance</asp:LinkButton>
                                                                <asp:Repeater ID="rptAllowances" runat="server" OnItemCommand="rptAllowances_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tbody id="tblAllowance">
                                                                                <tr class="tblheading">
                                                                                    <td>
                                                                                        Allowance Description
                                                                                    </td>
                                                                                    <td>
                                                                                        Ratio
                                                                                    </td>
                                                                                    <td>
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("AllowanceID")%>' CommandName="editallowance"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("AllowanceDescription")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("AllowanceID")%>' CommandName="editallowance"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                        <%# Eval("AllowanceRatio")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelAllowance" CommandArgument='<%# Eval("AllowanceID")%>'
                                                                                    CommandName="delallowance" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                        Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("AllowanceID")%>' CommandName="editallowance"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("AllowanceDescription")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("AllowanceID")%>' CommandName="editallowance"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                        <%# Eval("AllowanceRatio")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelAllowance" CommandArgument='<%# Eval("AllowanceID")%>'
                                                                                    CommandName="delallowance" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                        Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="3" ID="TabPanel4">
                                        <HeaderTemplate>
                                            Deductions
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlDeductionContent" runat="server">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Deduction Description
                                                                                    <asp:HiddenField ID="hdnDeductionID" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtDeductionDescription" ErrorMessage="Enter Deduction Description"
                                                                                    ControlToValidate="txtDeductionDescription" ValidationGroup="grpDeduction" runat="server"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtDeductionDescription" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RadioButton ID="rdbDeductionValue" Width="25px" GroupName="DeductionRatioType"
                                                                                    runat="server" /><label>
                                                                                        Value</label>
                                                                                <asp:RadioButton ID="rdbDeductionPercentage" Width="25px" GroupName="DeductionRatioType"
                                                                                    runat="server" /><label>
                                                                                        Percentage</label>
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Ratio
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:TextBox ID="txtDeductionRatio" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnSaveDeduction" ValidationGroup="grpDeduction" runat="server"
                                                                                    OnClick="btnSaveDeduction_Click">
                                                                                    <asp:Image ID="imgDeduction" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDiscardDeduction" runat="server" OnClick="btnDiscardDeduction_Click">Discard</asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlDeductionGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <input type="text" id="txtDeduction" name="n" style="width: 500px;" placeholder="Enter Text To Search " />
                                                                <asp:LinkButton ID="btnShowDeductionContent" OnClick="btnShowDeductionContent_Click"
                                                                    runat="server">Add New Deduction</asp:LinkButton>
                                                                <asp:Repeater ID="rptDeductions" runat="server" OnItemCommand="rptDeductions_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tbody id="tblDeduction">
                                                                                <tr class="tblheading">
                                                                                    <td>
                                                                                        Deduction Description
                                                                                    </td>
                                                                                    <td>
                                                                                        Ratio
                                                                                    </td>
                                                                                    <td>
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("DeductionID")%>' CommandName="editdeduction"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("DeductionDescription")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("DeductionID")%>' CommandName="editdeduction"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                        <%# Eval("DeductionRatio")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelDeduction" CommandArgument='<%# Eval("DeductionID")%>'
                                                                                    CommandName="deldeduction" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                        Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("DeductionID")%>' CommandName="editdeduction"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("DeductionDescription")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("DeductionID")%>' CommandName="editdeduction"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                        <%# Eval("DeductionRatio")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelDeduction" CommandArgument='<%# Eval("DeductionID")%>'
                                                                                    CommandName="deldeduction" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                        Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="4" ID="TabPanel5">
                                        <HeaderTemplate>
                                            Shifts
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlShiftContent" runat="server">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Shift Name
                                                                                    <asp:HiddenField ID="hdnShiftID" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtShiftName" ErrorMessage="Enter Shift Name"
                                                                                    ControlToValidate="txtShiftName" ValidationGroup="grpShift" runat="server"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtShiftName" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Shift Time From
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="rfv_txtDateTime" ForeColor="#FF0000" runat="server"
                                                                                    ValidationGroup="grpShift" ControlToValidate="txtShiftTimeFrom" ErrorMessage="Enter ShiftTimeFrom"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtShiftTimeFrom" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Active/InActive
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:DropDownList runat="server" ID="ddlActive">
                                                                                    <asp:ListItem Value="true" Enabled="true"> Active</asp:ListItem>
                                                                                    <asp:ListItem Value="false">InActive</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Shift Time To
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="#FF0000" runat="server"
                                                                                    ValidationGroup="grpShift" ControlToValidate="txtShiftTimeTo" ErrorMessage="Enter ShiftTime To"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtShiftTimeTo" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Late (Minutes)
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:TextBox ID="txtLate2" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="#FF0000" runat="server"
                                                                                    ValidationGroup="grpShift" ControlToValidate="txtLate2" ErrorMessage="Enter Late (Minutes)"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td width="20%">
                                                                            </td>
                                                                            <td width="30%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnSavShift" ValidationGroup="grpShift" runat="server" OnClick="btnSaveShift_Click">
                                                                                    <asp:Image ID="imgShift" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDiscardShift" runat="server" OnClick="btnDiscardShift_Click">Discard</asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlShiftGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <input type="text" id="txtShift" name="n" style="width: 500px;" placeholder="Enter Text To Search " />
                                                                <asp:LinkButton ID="btnShowpnlShiftContent" OnClick="btnShowpnlShiftContent_Click"
                                                                    runat="server">Add New Shift</asp:LinkButton>
                                                                <asp:Repeater ID="rptShift" runat="server" OnItemCommand="rptShift_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tbody id="tblShift">
                                                                                <tr class="tblheading">
                                                                                    <td>
                                                                                        Shift Name
                                                                                    </td>
                                                                                    <td>
                                                                                        Shift Time From
                                                                                    </td>
                                                                                    <td>
                                                                                        Shift Time To
                                                                                    </td>
                                                                                    <td>
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("ShiftID")%>' CommandName="editshift"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("ShiftName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany3" CommandArgument='<%# Eval("ShiftID")%>' CommandName="editshift"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Convert.ToDateTime(Eval("ShiftTimeFrom")).TimeOfDay%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany2" CommandArgument='<%# Eval("ShiftID")%>' CommandName="editshift"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%#Convert.ToDateTime(Eval("ShiftTimeTo")).TimeOfDay%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelShift" CommandArgument='<%# Eval("ShiftID")%>' CommandName="delshift"
                                                                                    runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("ShiftID")%>' CommandName="editshift"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("ShiftName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany3" CommandArgument='<%# Eval("ShiftID")%>' CommandName="editshift"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Convert.ToDateTime(Eval("ShiftTimeFrom")).TimeOfDay%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany2" CommandArgument='<%# Eval("ShiftID")%>' CommandName="editshift"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%#Convert.ToDateTime(Eval("ShiftTimeTo")).TimeOfDay%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelShift" CommandArgument='<%# Eval("ShiftID")%>' CommandName="delshift"
                                                                                    runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="5" ID="TabPanel6">
                                        <HeaderTemplate>
                                            Leaves
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlLeaveContent" runat="server">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Leave Type
                                                                                    <asp:HiddenField ID="hdnLeaveID" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="txtLeaveType_RequiredFieldValidator" ValidationGroup="grpLeave"
                                                                                    runat="server" ControlToValidate="txtLeaveType" ErrorMessage="Enter Leave Type"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtLeaveType" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Maximum Allowed
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:TextBox ID="txtMaximumAllowed" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Leave Payment Type
                                                                                    <asp:HiddenField ID="HiddenField5" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:DropDownList ID="ddlleavePaymentType" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Over Ride
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:DropDownList ID="ddlOverride" runat="server">
                                                                                    <asp:ListItem Value="true" Enabled="true">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="false">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td style="width: 20%">
                                                                                        </td>
                                                                                        <td style="width: 7%">
                                                                                            1 Leave =
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <asp:TextBox ID="txtLate" runat="server" Width="100%"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            Late(s)
                                                                                        </td>
                                                                                        <td style="width: 53%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 20%">
                                                                                        </td>
                                                                                        <td style="width: 7%">
                                                                                            1 Leave =
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <asp:TextBox ID="txtShort" runat="server" Width="100%"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            Short Leave(s)
                                                                                        </td>
                                                                                        <td style="width: 53%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 20%">
                                                                                        </td>
                                                                                        <td style="width: 7%">
                                                                                            1 Leave =
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            <asp:TextBox ID="txtHalf" runat="server" Width="100%"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%">
                                                                                            Half Day(s)
                                                                                        </td>
                                                                                        <td style="width: 53%">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td style="width: 20%">
                                                                                        </td>
                                                                                        <td style="width: 30%">
                                                                                            <asp:LinkButton ID="btnSaveLeave" ValidationGroup="grpLeave" runat="server" OnClick="btnSaveLeave_Click">
                                                                                                <asp:Image ID="imgLeave" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                                            <asp:LinkButton ID="btnDiscardLeave" runat="server" OnClick="btnDiscardLeave_Click">Discard</asp:LinkButton>
                                                                                        </td>
                                                                                        <td style="width: 50%">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlLeaveGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <input type="text" id="txtLeave" name="n" style="width: 500px;" placeholder="Enter Text To Search " />
                                                                <asp:LinkButton ID="btnShowpnlLeaveContent" OnClick="btnShowpnlLeaveContent_Click"
                                                                    runat="server">Add New Leave type</asp:LinkButton>
                                                                <asp:Repeater ID="rptLeave" runat="server" OnItemCommand="rptLeave_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tbody id="tblLeave">
                                                                                <tr class="tblheading">
                                                                                    <td>
                                                                                        Leave Type
                                                                                    </td>
                                                                                    <td>
                                                                                        Maximum Allowed
                                                                                    </td>
                                                                                    <td>
                                                                                        Leave Payment Type
                                                                                    </td>
                                                                                    <td>
                                                                                        Over Ride
                                                                                    </td>
                                                                                    <td>
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("LeaveID")%>' CommandName="editleave"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("LeaveType")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("LeaveID")%>' CommandName="editleave"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("MaximumAllowed")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("LeaveID")%>' CommandName="editleave"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("LeavePaymentTypeID")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton4" CommandArgument='<%# Eval("LeaveID")%>' CommandName="editleave"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("OverRide")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelLeave" CommandArgument='<%# Eval("LeaveID")%>' CommandName="delleave"
                                                                                    runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("LeaveID")%>' CommandName="editleave"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("LeaveType")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("LeaveID")%>' CommandName="editleave"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("MaximumAllowed")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("LeaveID")%>' CommandName="editleave"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("LeavePaymentTypeID")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton4" CommandArgument='<%# Eval("LeaveID")%>' CommandName="editleave"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("OverRide")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelLeave" CommandArgument='<%# Eval("LeaveID")%>' CommandName="delleave"
                                                                                    runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="6" ID="TabPanel7">
                                        <HeaderTemplate>
                                            Exceptions
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlExceptionContent" runat="server">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Exception Type
                                                                                    <asp:HiddenField ID="hdnExceptionID" runat="server" />
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtExceptionName" ValidationGroup="grpException"
                                                                                    runat="server" ControlToValidate="txtExceptionName" ErrorMessage="Enter Exception Type"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtExceptionName" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Number Of Hours
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:TextBox ID="txtNumberOfHours" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnSaveException" ValidationGroup="grpException" runat="server"
                                                                                    OnClick="btnSaveException_Click">
                                                                                    <asp:Image ID="imgException" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDiscardException" runat="server" OnClick="btnDiscardException_Click">Discard</asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlExceptionGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <input type="text" id="txtException" name="n" style="width: 500px;" placeholder="Enter Text To Search " />
                                                                <asp:LinkButton ID="btnShowpnlExceptionContent" OnClick="btnShowpnlExceptionContent_Click"
                                                                    runat="server">Add New Exception</asp:LinkButton>
                                                                <asp:Repeater ID="rptException" runat="server" OnItemCommand="rptException_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tbody id="tblException">
                                                                                <tr class="tblheading">
                                                                                    <td>
                                                                                        Exception Type
                                                                                    </td>
                                                                                    <td>
                                                                                        Number Of Hours
                                                                                    </td>
                                                                                    <td>
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("ExceptionID")%>' CommandName="editexception"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("ExceptionDescription")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton6" CommandArgument='<%# Eval("ExceptionID")%>' CommandName="editexception"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("NumberOfHours")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelException" CommandArgument='<%# Eval("ExceptionID")%>'
                                                                                    CommandName="delexception" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("ExceptionID")%>' CommandName="editexception"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("ExceptionDescription")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton6" CommandArgument='<%# Eval("ExceptionID")%>' CommandName="editexception"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("NumberOfHours")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelException" CommandArgument='<%# Eval("ExceptionID")%>'
                                                                                    CommandName="delexception" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="7" ID="TabPanel9">
                                        <HeaderTemplate>
                                            Salary Templates
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlSalaryStructureContent" runat="server" ScrollBars="Auto" Height="365px">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Template Name
                                                                            </td>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtSalaryStructureName" ValidationGroup="grpTemplate"
                                                                                    runat="server" ControlToValidate="txtSalaryStructureName" ErrorMessage="Enter Template Name"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtSalaryStructureName" runat="server"></asp:TextBox>
                                                                                <asp:HiddenField ID="hdnSalaryStructureID" runat="server" />
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
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtAllowanceAmount" ValidationGroup="grpTemplate"
                                                                                                    runat="server" ControlToValidate="txtAllowanceAmount" ErrorMessage="Enter  Allowance Amount"></asp:RequiredFieldValidator>
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
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtDeductionAmount" ValidationGroup="grpTemplate"
                                                                                                    runat="server" ControlToValidate="txtDeductionAmount" ErrorMessage="Enter Allowance Amount"></asp:RequiredFieldValidator>
                                                                                                <asp:TextBox ID="txtDeductionAmount" Text='<%# Eval("DeductionRatio") %>' runat="server"></asp:TextBox>
                                                                                                <asp:HiddenField ID="hdnAllDeductionID" Value='<%# Eval("DeductionID") %>' runat="server">
                                                                                                </asp:HiddenField>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtComments" TextMode="MultiLine" Text='<%# Eval("Comments") %>'
                                                                                                    placeholder="Enter Comments" runat="server"></asp:TextBox>
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
                                                                                <asp:LinkButton ID="btnSaveSalaryStructure" ValidationGroup="grpTemplate" runat="server"
                                                                                    OnClick="btnSaveSalaryStructure_Click">
                                                                                    <asp:Image ID="imgSalaryStructure" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDiscardSalaryStructure" runat="server" OnClick="btnDiscardSalaryStructure_Click">Discard</asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlSalaryStructureGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <input type="text" id="txtSalaryStructure" name="n" style="width: 500px;" placeholder="Enter Text To Search " />
                                                                <asp:LinkButton ID="btnShowpnlSalaryStructureContent" runat="server" OnClick="btnShowpnlSalaryStructureContent_Click">Add New Salary Structure Template</asp:LinkButton>
                                                                <asp:Repeater ID="rptSalaryStructure" runat="server" OnItemCommand="rptSalaryStructure_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tbody id="tblSalaryStructure">
                                                                                <tr class="tblheading">
                                                                                    <td>
                                                                                        Salary Template Name
                                                                                    </td>
                                                                                    <td>
                                                                                        Action
                                                                                    </td>
                                                                                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("SalaryStructureID")%>'
                                                                                    CommandName="editsalarytemplate" runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("SalaryStructureName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelSalaryTemplate" CommandArgument='<%# Eval("SalaryStructureID")%>'
                                                                                    CommandName="delsalarytemplate" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnEditCompany" CommandArgument='<%# Eval("SalaryStructureID")%>'
                                                                                    CommandName="editsalarytemplate" runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("SalaryStructureName")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnDelSalaryTemplate" CommandArgument='<%# Eval("SalaryStructureID")%>'
                                                                                    CommandName="delsalarytemplate" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                     Delete
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="7" ID="TabPanel8">
                                        <HeaderTemplate>
                                            Provident Fund
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" class="cmp">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <asp:Panel ID="pnlPFContent" runat="server">
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr height="15px">
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Maximum Amount
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtMaximumAmount" ErrorMessage="Enter Maximum Amount"
                                                                                    ControlToValidate="txtMaximumAmount" ValidationGroup="grpPF" runat="server"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtMaximumAmount" runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td width="20%">
                                                                                <label>
                                                                                    Provident Fund(%)
                                                                                </label>
                                                                            </td>
                                                                            <td width="30%">
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtPF" ForeColor="#FF0000"
                                                                                    runat="server" ValidationGroup="grpPF" ControlToValidate="txtPF" ErrorMessage="Enter Provident Fund"></asp:RequiredFieldValidator>
                                                                                <asp:TextBox ID="txtPF" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnSavePF" ValidationGroup="grpPF" runat="server" OnClick="btnSavePF_Click">
                                                                                    <asp:Image ID="imgPF" runat="server" ImageUrl="../images/btn-save.png" /></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDiscardPF" runat="server" OnClick="btnDiscardPF_Click">Discard</asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlPFGrid" runat="server" ScrollBars="Auto" Height="365px">
                                                                <asp:LinkButton ID="btnShowpnlPFContent" OnClick="btnShowpnlPFContent_Click" runat="server">Add Provident Fund</asp:LinkButton>
                                                                <asp:Repeater ID="rptPF" runat="server" OnItemCommand="rptPF_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table style="width: 100%; margin-top: 0px">
                                                                            <tr class="tblheading">
                                                                                <td>
                                                                                    Maximum Amount
                                                                                </td>
                                                                                <td>
                                                                                    Provident Fund(%)
                                                                                </td>
                                                                            </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnAddPF" CommandArgument='<%# Eval("PFID")%>' CommandName="UnitID"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("MaximumAmount")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnAddPF2" CommandArgument='<%# Eval("PFID")%>' CommandName="UnitID"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("Percentage")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <tr style="background-color: #E4E4E4">
                                                                            <td>
                                                                                <asp:LinkButton ID="btnAddPF" CommandArgument='<%# Eval("PFID")%>' CommandName="UnitID"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("MaximumAmount")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <asp:LinkButton ID="btnAddPF2" CommandArgument='<%# Eval("PFID")%>' CommandName="UnitID"
                                                                                    runat="server" ClientIDMode="AutoID">
                                                                                     <%# Eval("Percentage")%>
                                                                                </asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </AlternatingItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function pageLoad() {

            var val = '<%=Session["CompanyLogoPath"] %>';
            if (val == '') {
            } else {
                var logo = '<%= Session["CompanyLogoPath"]%>';
                $('#pic').attr('src', '../CompanyLogos/' + logo).width(75).height(68);

                //find the current popup
                var popUp = $find('ModelPopup');

                //check it exists so the script won't fail
                if (popUp) {
                    //Add the function below as the event
                    popUp.add_hidden(HidePopupPanel);
                }
            }

            $("#<%=txtShiftTimeFrom.ClientID %>").mask("99:99");
            $("#<%=txtShiftTimeTo.ClientID %>").mask("99:99");

            ///////////Search for Department////////////////////     
            $("#txtDepartment").keyup(function (e) {
                $("#tblDepartment tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtDepartment").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblDepartment tr:has(td)").show();
                    return false;
                }
                $("#tblDepartment tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });

            ///////////Search for Designation////////////////////     
            $("#txtDesignation").keyup(function (e) {
                $("#tblDesignation tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtDesignation").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblDesignation tr:has(td)").show();
                    return false;
                }
                $("#tblDesignation tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });

            ///////////Search for Allowance////////////////////     
            $("#txtAllowance").keyup(function (e) {
                $("#tblAllowance tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtAllowance").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblAllowance tr:has(td)").show();
                    return false;
                }
                $("#tblAllowance tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });

            ///////////Search for Deduction////////////////////     
            $("#txtDeduction").keyup(function (e) {
                $("#tblDeduction tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtDeduction").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblDeduction tr:has(td)").show();
                    return false;
                }
                $("#tblDeduction tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });


            ///////////Search for Shift////////////////////     
            $("#txtShift").keyup(function (e) {
                $("#tblShift tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtShift").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblShift tr:has(td)").show();
                    return false;
                }
                $("#tblShift tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });


            ///////////Search for Leave////////////////////     
            $("#txtLeave").keyup(function (e) {
                $("#tblLeave tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtLeave").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblLeave tr:has(td)").show();
                    return false;
                }
                $("#tblLeave tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });


            ///////////Search for Exception////////////////////     
            $("#txtException").keyup(function (e) {
                $("#tblException tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtException").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblException tr:has(td)").show();
                    return false;
                }
                $("#tblException tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });


            ///////////Search for SalaryStructure////////////////////     
            $("#txtSalaryStructure").keyup(function (e) {
                $("#tblSalaryStructure tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#txtSalaryStructure").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#tblSalaryStructure tr:has(td)").show();
                    return false;
                }
                $("#tblSalaryStructure tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });
        }
    </script>
</asp:Content>
