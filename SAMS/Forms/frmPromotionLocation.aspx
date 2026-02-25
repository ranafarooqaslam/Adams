<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmPromotionLocation.aspx.cs" Inherits="Forms_frmPromotionLocation" Title="SAMS :: Shift Promotion Location" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    
    <script type="text/javascript">
        var TotalChkBx = 0;
        var Counter;

        function pageLoad() {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%=Grid_pricedetails.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.Grid_pricedetails.ClientID %>');
            var TargetChildControl = "cbExtend";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter; 
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="float:left" width="40%" cellpadding="4">
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lblPrincipal" runat="server">Principal</asp:Label>
                            </strong>
                        </td>
                        <td style="width: 30%;">
                            <asp:DropDownList ID="DrpPrincipal" runat="server" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="width:40%"></td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 30%;">
                            <asp:CheckBox ID="ChbActive" runat="server" Text="Is Active"></asp:CheckBox>
                        </td>
                        <td style="width:40%"></td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 30%;">
                            <asp:Button ID="btnPromotion" OnClick="btnPromotion_Click" runat="server" Width="125px"
                                Font-Size="8pt" Text="Get Promotion" CssClass="Button" />
                        </td>
                        <td style="width:40%"></td>
                    </tr>
                </table>
                <table style="float:right;" width="60%" cellpadding="4">
                    <tr>
                        <td style="width: 10%;">
                            <strong>
                                <asp:Label ID="lbltoLocation" runat="server" Text="Location" CssClass="lblbox"></asp:Label></strong>
                        </td>
                        <td style="width: 30%; height: 25px">
                            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" CssClass="DropList">
                            </asp:DropDownList>
                        </td>
                        <td style="width:40%"></td>
                    </tr>
                    <tr>
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                        </td>
                        <td style="width: 30%;">
                            <asp:Button ID="btnAssign" runat="server" Width="125px"
                                Font-Size="8pt" Text="Assign" CssClass="Button" OnClick="btnAssign_Click" />
                        </td>
                        <td style="width:40%"></td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <table class="tblhead" width="980px">
                                <tbody>
                                    <tr>
                                        <td style="color: White; font-weight: bold;">
                                            <asp:Label ID="Label10" runat="server" Width="94px" Text="Searching Type"></asp:Label>
                                        </td>
                                        <td style="width: 170px; height: 21px" align="left">
                                            <asp:DropDownList ID="ddSearchType" runat="server" Width="154px" CssClass="DropList">
                                                <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                                <asp:ListItem Value="SCHEME_DESC">Scheme</asp:ListItem>
                                                <asp:ListItem Value="PROMOTION_ID">Promotion Id</asp:ListItem>
                                                <asp:ListItem Value="PROMOTION_CODE">Promotion Code</asp:ListItem>
                                                <asp:ListItem Value="PROMOTION_DESCRIPTION">Description</asp:ListItem>
                                                <asp:ListItem>Principal</asp:ListItem>
                                                <asp:ListItem Value="Promotion_Class_Code">Promotion Class Code</asp:ListItem>
                                                <asp:ListItem Value="Promotion_Class_Name">Promotion Class Name</asp:ListItem>
                                                <asp:ListItem Value="GROUP_NAME">GROUP NAME</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 224px; height: 21px" align="left">
                                            <asp:TextBox ID="txtSeach" runat="server" Width="180px" CssClass="txtBox "></asp:TextBox>
                                        </td>
                                        <td style="height: 21px" align="left" width="250">
                                            <asp:Button ID="btnFilter" OnClick="btnFilter_Click" runat="server" Width="85px"
                                                Font-Size="8pt" Text="Filter"></asp:Button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>                                
                            <asp:Panel ID="Panel2" runat="server" Width="99.9%" Height="300px" ScrollBars="Vertical"
                                BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                <asp:GridView ID="Grid_pricedetails" runat="server" Width="99%" ForeColor="SteelBlue"
                                    CssClass="gridRow2" BackColor="White" BorderColor="White" HorizontalAlign="Center"
                                    AutoGenerateColumns="False">
                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                        PreviousPageText="Previous"></PagerSettings>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbExtend" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbHeader" onclick="javascript:HeaderClick(this);" runat="server" />
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PROMOTION_ID" HeaderText="Promotion Id">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SCHEME_DESC" HeaderText="Scheme">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROMOTION_CODE" HeaderText="Promotion Name">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROMOTION_DESCRIPTION" HeaderText="Description">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="START_DATE" HeaderText="Start Date">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="END_DATE" HeaderText="End Date">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Principal" HeaderText="Principal">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SCHEME_ID" HeaderText="Scheme Id">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                </asp:GridView>
                            </asp:Panel>                            
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Grid_pricedetails"  />
                </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
