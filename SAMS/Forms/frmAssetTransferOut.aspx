<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmAssetTransferOut.aspx.cs" Inherits="Forms_frmAssetTransferOut" Title="SAMS :: Asset Transfer Out" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<style>
   .ajax__calendar_container { z-index : 1000 ; }
</style>

<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>

    <script language="JavaScript" type="text/javascript">

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

        function Check_Click(objRef) {
            var row = objRef.parentNode.parentNode;
            var GridView = row.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var headerCheckBox = inputList[0];
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }

        function ValidateForm() {
           <%-- var str;
            str = document.getElementById('<%=txtAssetType.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Asset Type');
                return false;
            }--%>
            return true;
        }
        function ddlFocus(obj) {
            obj.className = "ddlFocus";
        }

        function ddlBlur(obj) {
            obj.className = "";

        }
        function pageLoad() {
            $("select").searchable();
            $("input:text").keypress(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        }
    </script>
    <div id="right_data">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                         <tr>
                                            <td align="left">
                                                <strong>
                                                <asp:Label ID="lbltoLocation" runat="server" Width="94px" Text="From Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td style="width:10%">
                                            <strong>
                                                    <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" Width="94px"></asp:Label>
                                            </strong>
                                        </td>
                                            <td style="width:25%">
                                            <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" CssClass="DropList"
                                                OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <strong>
                                                <asp:Label ID="Label2" runat="server" Width="94px" Text="Location Type" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributorType" runat="server" Width="200px" AutoPostBack="true"
                                                     CssClass="DropList" OnSelectedIndexChanged="drpDistributorType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left">
                                                <strong>
                                                <asp:Label ID="Label1" runat="server" Width="94px" Text="To Location" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="drpDistributor1" runat="server" Width="200px" CssClass="DropList"
                                                    OnSelectedIndexChanged="drpDistributor1_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Deliver to Customer? <asp:CheckBox runat="server" ID="chkDeliverCustomer" Checked="false" OnCheckedChanged="chkDeliverCustomer_Changed" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="80px" Height="13px" Text="Transfer Date"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" align="left"><asp:TextBox ID="txtTransferDate" onkeyup="BlockEndDateKeyPress()" runat="server"
                                                Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox>
                                                <asp:ImageButton ID="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                            </td>
                                            <cc1:CalendarExtender ID="CEEndDate" runat="server" TargetControlID="txtTransferDate"
                                                PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                            </cc1:CalendarExtender>
                                        </tr>
                                        <tr runat="server" id="customerRow">
                                            <td align="left">
                                                <strong>
                                                <asp:Label ID="Label3" runat="server" Width="94px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            
                                        </tr>
                                        <tr runat="server" id="issuanceRow">
                                            <td align="left">
                                                <strong>
                                                <asp:Label ID="Label4" runat="server" Width="94px" Text="Issuance On Account" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="width: 238px; height: 25px" align="left">
                                                <asp:TextBox runat="server" ID="txtIssuance" Width="192px"></asp:TextBox>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="lblDescription" runat="server" Width="80px" Text="Remarks"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 25px" colspan="3">
                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" Width="300px"></asp:TextBox>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="height: 25px">
                                            </td> 
                                            <td style="height: 25px">
                                                 <asp:CheckBox ID="chkSerial" runat="server" Width="150px" Text="Is Serial No. based" AutoPostBack="True"
                                                    Checked="True" OnCheckedChanged="chkSerial_CheckChanged"></asp:CheckBox>
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
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
            <table width="100%">
                 <td>
                        <table>
                            <tbody>
                <tr runat="server" id="nonSerialBasedRow" visible="false">
                    <td>
                        <strong>
                            <asp:Label ID="Label6" runat="server" Text="Asset" Width="100px"
                                CssClass="lblbox"></asp:Label>
                            </strong>
                    </td>
                    <td style="width:250px;">
                        <asp:DropDownList ID="DrpAsset" Width="100%" runat="server" CssClass="DropList">
                        </asp:DropDownList>
                    </td>

                    <td>
                        <strong>
                            <asp:Label ID="Label7" runat="server" Text="Qty" Width="30px"
                                CssClass="lblbox"></asp:Label></strong>
                    </td>
                   <td>
                        <asp:TextBox ID="txtQty" runat="server" Width="100px" CssClass="txtBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button AccessKey="A" ID="btnNonSerial" OnClick="btnNonSerial_Click" runat="server"
                            Font-Size="8pt" Text="Add" ValidationGroup="vg" CssClass="Button" />
                    </td>
                </tr>
                                </tbody>
                            </table>
                     </td>
                <tr runat="server" id="nonSerialGridView" visible="false">
                    <td>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="left" colspan="8">
                                        <asp:Panel ID="Panel1" runat="server" Width="750px" Height="250px" ScrollBars="Vertical"
                                            BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                            <asp:GridView ID="grdViewNonSerial" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                OnRowCommand="grdNonSerial_RowCommand"
                                                ShowHeader="true" Width="725px">
                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                    PreviousPageText="Previous" />
                                                <RowStyle ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                         <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                        <%--<ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" onclick="Check_Click(this)" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle BackColor="#006699" ForeColor="White" Font-Bold="true" />
                                                        <ItemStyle Width="45px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />--%>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Asset_Marking_ID" ReadOnly="true" HeaderText="Asset_Marking_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Asset_PurchaseDetail_ID" ReadOnly="true" HeaderText="Asset_PurchaseDetail_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AssetTypeID" ReadOnly="true" HeaderText="AssetTypeID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AssetTypeName" ReadOnly="true" HeaderText="Asset">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SerialNo1" ReadOnly="true" HeaderText="Serial #.1">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SerialNo2" ReadOnly="true" HeaderText="Serial #.2">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Rate" ReadOnly="true" HeaderText="Rate">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Qty" ReadOnly="true" HeaderText="Qty">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Color" ReadOnly="true" HeaderText="Color">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MfgYear" ReadOnly="true" HeaderText="Mfg. Year">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CompanyAssetNo" ReadOnly="true" HeaderText="Company Asset No">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ID" ReadOnly="true" HeaderText="ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Purchase_Date" ReadOnly="true" HeaderText="Purchase Date">
                                                      <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Center"
                                                            Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="REF_ID" ReadOnly="true" HeaderText="REF_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt" Text="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                            Width="35px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" />
                                                <PagerStyle BackColor="Transparent" />
                                                <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                    VerticalAlign="Middle" />
                                                <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="serialBasedRow">
                    <td>
                        <table>
                            <tbody>
                                <tr>
                                    <td align="left" colspan="8">
                                        <asp:Panel ID="Panel2" runat="server" Width="750px" Height="250px" ScrollBars="Vertical"
                                            BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                            <asp:GridView ID="GridAssetType" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                OnRowDeleting="GridAssetType_RowDeleting" OnRowCommand="GridAssetType_RowCommand"
                                                ShowHeader="true" Width="725px">
                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                    PreviousPageText="Previous" />
                                                <RowStyle ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" onclick="Check_Click(this)" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle BackColor="#006699" ForeColor="White" Font-Bold="true" />
                                                        <%--                                                                style="display:inline-block;color:White;background-color:#006699;font-weight:bold;height:16px;width:250px;"--%>
                                                        <ItemStyle Width="45px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Asset_Marking_ID" ReadOnly="true" HeaderText="Asset_Marking_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Asset_PurchaseDetail_ID" ReadOnly="true" HeaderText="Asset_PurchaseDetail_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AssetTypeID" ReadOnly="true" HeaderText="AssetTypeID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AssetTypeName" ReadOnly="true" HeaderText="Asset">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SerialNo1" ReadOnly="true" HeaderText="Serial #.1">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            Width="75px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SerialNo2" ReadOnly="true" HeaderText="Serial #.2">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                            Width="75px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Rate" ReadOnly="true" HeaderText="Rate">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Color" ReadOnly="true" HeaderText="Color">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MfgYear" ReadOnly="true" HeaderText="Mfg. Year">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CompanyAssetNo" ReadOnly="true" HeaderText="Company Asset No">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                            Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ID" ReadOnly="true" HeaderText="ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Purchase_Date" ReadOnly="true" HeaderText="Purchase Date">
                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                            Width="85px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="REF_ID" ReadOnly="true" HeaderText="REF_ID">
                                                        <HeaderStyle CssClass="HidePanel" />
                                                        <ItemStyle CssClass="HidePanel" />
                                                    </asp:BoundField>
                                                    <%--<asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt"  Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center"
                                                                    Width="35px" />
                                                            </asp:TemplateField>--%>
                                                    <%-- <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                        Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                            </asp:TemplateField>--%>
                                                </Columns>
                                                <FooterStyle BackColor="White" />
                                                <PagerStyle BackColor="Transparent" />
                                                <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                    VerticalAlign="Middle" />
                                                <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <tr>
                            <td>
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Width="119px" Font-Size="8pt"
                                    Text="Save Document" OnClick="btnSaveDocument_Click" CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                    Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />
                            </td>
                        </tr>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveDocument" />
                    </Triggers>
                </asp:UpdatePanel>
            </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
