<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmBulkCustomerUpdate.aspx.cs" Inherits="Forms_frmBulkCustomerUpdate" Title="SAMS :: Update Customer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript" src="../AjaxLibrary/jquery.searchabledropdown-1.0.8.min.js"></script>
    <script language="JavaScript" type="text/javascript">

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

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnLoadData.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnLoadData.ClientID%>').disabled = false;
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
                                <table width="100%">
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Text="Location"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px" 
                                                OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Town"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:50%">
                                            <asp:DropDownList ID="DrpTown" runat="server" Width="205px" OnSelectedIndexChanged="DrpTown_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:10%">
                                            <strong>
                                                <asp:Label ID="Label9" runat="server" Text="Route"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="DrpRoute" runat="server" Width="205px" 
                                                OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td align="left" colspan="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 143px" align="left">
                                            </td>
                                            <td style="width: 175px">
                                                <asp:Button ID="btnLoadData" OnClick="btnLoadData_Click" runat="server" Width="80px"
                                                    Font-Size="8pt" Text="Load Data" CssClass="Button" />
                                                 &nbsp;
                                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px" Font-Size="8pt"
                                                    Text="Save" ValidationGroup="vg" CssClass="Button" />
                                               
                                                
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 219px">
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 244px;
                            height: 100px">
                            &nbsp;<asp:Panel ID="Panel21" runat="server">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                            Width="23px" />
                                        Wait Update
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" Height="400px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter"
    HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="White" OnRowDataBound="Grid_users_DataBound">
    
        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
        PreviousPageText="Previous" />
    <RowStyle ForeColor="Black" />
    <Columns>
        <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="BUSINESS_TYPE_ID" HeaderText="BUSINESS_TYPE_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="PROMOTION_CLASS" HeaderText="PROMOTION_CLASS">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="TOWN_ID" HeaderText="TOWN_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="AREA_ID" HeaderText="AREA_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="ROUTE_ID" HeaderText="ROUTE_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="Email">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS">
            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="GST_NUMBER" HeaderText="Gst No">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="ChannelType" HeaderText="Channel Type">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="GEO_NAME" HeaderText="Town">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="AREA_NAME" HeaderText="Route">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="ROUTE_NAME" HeaderText="Market">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:BoundField>
        <asp:BoundField DataField="REGDATE">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="IS_STAND" HeaderText="Stand">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="IS_COOLER" HeaderText="Cooler">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="CNIC" HeaderText="CNIC">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="NTN" HeaderText="NTN">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="GST" HeaderText="GST">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="Latitude">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="Longitude">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="IsShifted">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="TAX_SLAB_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:BoundField DataField="CLAUSE_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Tax Slab">
            <ItemTemplate>
                <asp:DropDownList ID="DrpTaxSlab" runat="server" Width="205" AutoPostBack="true" OnSelectedIndexChanged="DrpTaxSlab_SelectedIndexChanged">
                </asp:DropDownList>
            </ItemTemplate>
            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Tax Clause">
            <ItemTemplate>
                <asp:DropDownList ID="DrpSlabType" runat="server" Width="205">
                </asp:DropDownList>
            </ItemTemplate>
            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="White" />
    <PagerStyle BackColor="Transparent" />
    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395"
        Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
</asp:GridView>

                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
