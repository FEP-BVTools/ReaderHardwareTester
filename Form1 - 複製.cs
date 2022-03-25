using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

struct UsedAmount       //一卡通Maas StartEndStation使用者結構體
{
    public byte MaxUsage; // 使用次數上限   MAX 255
    public byte RemainingUse; // 剩餘使用次數   MAX 255
    public byte[] UsedValue; // 累積使用金額   MAX 65535
}
struct StartEndStation  //一卡通Maas StartEndStation結構體
{
    public byte TransportSetting;   // 起訖站交通運具 MAX 255, 若無使用則填入0x00
    public byte[] StartStation;       // 起站 MAX 1023, 若無使用則填入0x00, 0x00
    public byte[] EndStation;         // 訖站 MAX 1023, 若無使用則填入0x00, 0x00
    public byte[] RouteID;            // 路線編號, 若無使用則填入0x00, 0x00
    public UsedAmount Used; // 使用次數 or 累積使用金額, 若無使用則填入0x00, 0x00
}
struct KRTC_Read     //一卡通讀卡結構體
{
    public byte[] csn;           // 卡號，16 bytes
    public byte[] evValue;        // 主要電子票值4
    public byte[] bevValue;       // 備份電子票值4
    public byte[] syncValue;      // 同步後電子票值4
    public byte[] persionalID;        // 身份證字號6
    public byte cardType;          // 公車端票卡種類，1 byte
    public byte speProviderType;       // 特種票識別身分，1 byte
    public byte speProviderID;     // 特種票識別單位，1 byte
    public byte[] speActivationTime;  // 特種票識別起始日，4 bytes(年-2bytes, 月-1byte, 日-1 byte)
    public byte[] speExipreTime;      // 特種票識別有效日，4 bytes(年-2bytes, 月-1byte, 日-1 byte)
    public byte[] speResetTime;       // 特種票重置日期，4 bytes(年-2bytes, 月-1byte, 日-1 byte)
    public byte spestStation;      // 特種票起站代碼，1 byte
    public byte speedStation;      // 特種票迄站代碼，1 byte
    public byte[] speRouteID;     // 特種票路線編號，2 bytes
    public byte[] speUseNo;       // 特種票使用限次，2 bytes
    public byte busConveyancer;        // 公車業者代碼，1 byte，2012.05.14_2.0.04 新增
    public byte preBUSAreaCode;        // 上次公車端區碼，1 byte
    public byte preBUSCompID;      // 上次公車端交易運輸業者，1 byte
    public byte[] preBUSRoute;        // 上次公車端交易路線，2 byte
    public byte preBUSStation;     // 上次公車端交易站號，1 byte
    public byte[] preBUSTime;     // 上次公車端交易時間(UNIX Time)，4 bytes
    public byte preBUSStatus;      // 上次公車端搭乘狀態，0x00-一般(已下車), 0x01-乘車中(已上車)
    public byte preTravelType;     // 上次搭乘類型，1 byte(0x01-錢包搭乘, 0x02-特種票搭乘等等)
    public byte[] preBUSNo;       // 上次公車端交易驗票機編號，2 bytes
    public byte[] speHUsedNo;     // 特種票已使用次數，2 bytes
    public byte[] cardTxnsNumber;     // 卡片交易序號，2 byte，S3B0#1 ，2012.07.07_V2.0.01  新增
    public byte[] preGenTime;     // 上次交易時間(UNIX Time)，4 bytes，S4B0#2
    public byte preGenClass;       // 上次交易類別，1 byte，S4B0#3
    public byte[] preTxnsValue;       // 上次交易票值/票點，2 byte，S4B0#4 ，2012.07.07_V2.0.01  新增
    public byte[] prePostValue;       // 上次交易後票值/票點，2 byte，S4B0#5 ，2012.07.07_V2.0.01  新增
    public byte preGenSystem;      // 上次交易系統編號，1 byte，S4B0#6
    public byte preTxnsLocaion;        // 上次交易地點編號，1 byte，S4B0#7 ，2012.07.07_V2.0.01  新增
    public byte[] preTxnsMachine;     // 上次交易機器編號，4 byte，S4B0#8 ，2012.07.07_V2.0.01  新增
    public byte preSale;           // 計程預收金額，1 byte
    public byte syncStatus;        // 同步狀態，0-無須同步，1-表主蓋備，2-表備蓋主
    public byte[] trfID;          // 轉乘ID 2 byte (Little Endian)
    public byte transferFlag;      // 轉換旗標，1 byte，0表無須轉換，1表A3洗點，2表C3洗卡洗點，3表C4洗卡洗點，4表C5洗卡//2013.09.05_V2.2.0.0
    public byte travelDay;         // 旅遊卡天數，2013.09.05_V2.2.0.0
    public byte[] traExpireTime;      // 旅遊卡有效日，2013.09.05_V2.2.0.0
    public byte[] personalTime;       // 個人化有效日，2013.09.05_V2.2.0.0

    // 2012.05.14 新增_v2.0.04_項目六：新增段次交易所需欄位資料
    // *****START*****
    public byte[] trfGenTime;     // (轉乘識別群組#1)交易時間(UNIX Time)，4 bytes，S3B0#3(byte3~6)
    public byte trfspeCurrentSystemID; // (轉乘識別群組#2)本次系統代碼，1 bytes，S3B0#4(byte7)
    public byte trfprePreviousSystemID;    // (轉乘識別群組#3)前次系統代碼，1 bytes，S3B0#5(byte8)
    public byte trfTxnsType;       // (轉乘識別群組#4)交易類別，1 bytes，S3B0#6(byte9)
    public byte trfCompID;         // (轉乘識別群組#5)交易業者編號，1 bytes，S3B0#7(byte10)
    public byte trfTxnsLocation;       // (轉乘識別群組#6)交易地點編號，1 bytes，S3B0#8(byte11)
    public byte[] trfRouteID;     // (轉乘識別群組#7)交易路線編號，2 bytes，S3B0#8(byte12~13)
    public byte[] trfTxnsMachine;     // (轉乘識別群組#8)交易設備編號，2 bytes，S3B0#9(byte14~15)
                                      // *****END*****

    // 2.2.1.7 <AC01> 新增數位學生證與記名學生卡識別資訊
    public byte regFlag;                   // 記名旗標 (S1B1B14)
    public byte cardStatus;                // 卡片狀態 (S1B0B14)
    public byte identifyRegStudentCard;    // 記名學生卡識別 (見一覽表B 7.2 學生卡綜合判斷說明)
                                           // 0x00表示非記名卡，
                                           // 0x01不記名學生卡已記名(記名學生卡)
                                           // 0x02數位學生證(記名學生卡)

    // 2.2.1.7 <AD01> 新增讀出定期票業者代碼
    public byte monthlyCompID;             // 定期票業者代碼 (S8B0B13)
    // 2.2.2.2 Add `evenRideBit' for Taoyuan Citizen Card.
    public byte evenRideBit;

    public byte MaaSCard;      // MaaS卡種類
    public byte MappingType;   // MaaS 0x01
    public byte MaaSAreaCode;      // 區域代碼
    public byte[] MaaSTransport;  // 交通運具
    public byte MaaSPeriodCode;    // 票種旗標
    public byte MaaSPeriod;        // 天數/時數 MAX 255
    public byte[] MaaSStartDate;  // 起始時間 UNIX Time (Little Endian)
    public byte[] MaaSEndDate;    // 結束時間 UNIX Time (Little Endian)
    public StartEndStation MaaSStartEndStation1, MaaSStartEndStation2, MaaSStartEndStation3, MaaSStartEndStation4;// 起迄站資訊
    //2018.02.13_V2.2.2.4
    public byte[] CardNumber; // 外觀卡號, EV1才會填入
    //2020.02.14_V2.2.2.9
    public byte[] EnterpriseCode;// 企業代碼  (Little Endian)
    public byte CardVersion; // 票卡版本
    public byte[] CardTransactionSerialNumber; //票卡交易序號  (Little Endian)
    public byte SixRecordIndex; // 六筆寫入指標
    public byte PersonalType; // 個人身分別
    public byte[] CardExpireTime; //  票卡有效日期 UNIX Time (Little Endian)
    public byte BankID; // 銀行代碼

}
struct KRTC_Write    //一卡通寫卡結構體
{
    public byte[] csn;   // 卡號，16 bytes，如S0B0#1,2
    public byte[] txnsValue;   // 交易金額，2 bytes
    public byte[] txnsTime;   // 交易時間(UNIX Time)，4 bytes
    public byte txnsType;   // 交易類別，1 byte，如S4B0#3
    public byte txnsSystem;   // 交易系統編號，1 byte，如S4B0#6
    public byte txnsLocation;   // 交易地點編號，1 byte，如S4B0#7
    public byte[] txnsMachine;   // 交易機器編號，2 bytes，如S4B0#8
    public byte compID;   // 運輸業者編號，1 byte，目前有14家業者
    public byte[] routeID;   // 交易路線編號，2 bytes，由業者自訂
    public byte travelType;   // 搭乘類型，1 bytes (0x01-錢包搭乘、0x02-特種票搭乘等)
    public byte[] speHUsedNo;   // 特種票已使用次數，2 byte，若無使用則填入0x00, 0x00
    public byte[] speResetTime;   // 特種票重置日期，4 bytes(年-2bytes, 月-1byte, 日-1 byte)
    public byte permitFlag;   // 特許旗標
    public byte areaCode;   // 區碼
    public byte[] trfID;     //轉乘ID 2 byte (Little Endian)
    public byte txnspreSale;   // 計程預收金額，1 byte //2012.05.14 新增_v2.0.04_項目五
    public byte[] traExpireTime;   // 暢遊卡有效日，4 bytes(UNIX Time)，若無使用則寫0，若有使用則填入有效日期, 2013.09.05_V2.2.0.0
    public byte[] welExpireTime;   // 社福卡有效日，4 bytes(UNIX Time)，若無使用則寫0，若有使用則填入有效日期, 2013.09.05_V2.2.0.0
    public byte[] speUseNo;   // 2015.11.26 新增點數上限更新

    // 2.2.2.2 Add `evenRideBit' for Taoyuan Citizen Card.
    public byte incrEvenRideBit; // Refer to `EvenRideBitOptions' below.
    public StartEndStation MaaSStartEndStation1, MaaSStartEndStation2, MaaSStartEndStation3, MaaSStartEndStation4;
}
struct ICASH_Record  //愛金卡讀卡結構體
{
    public byte[] Card_No;              //icasH2.0卡號 (BCD) 8bytes 5
    public byte[] Card_Balance;         //票卡卡片額度 4bytes int 13
    public byte[] Card_TSN;             //票卡交易序號 4bytes uint 17
    public byte Card_Type;            //卡片種類 1byte 21
    public byte[] Card_Expiration_Date; //卡片有效日期 4bytes 22
    public byte Kind_of_the_ticket;   //票卡種類 1byte 26
    public byte Area_Code;            //區碼 1byte 27
    public byte[] Points_of_Card;       //票卡點數(敬老、愛陪) 2bytes ushort 28
    public byte[] PersonalID;           //身分證字號 10bytes 30
    public byte ID_Code;              //身分識別碼 1byte 40
    public byte Card_Issuer;          //發卡單位 1byte 41
    public byte[] Begin_Date;           //啟用日期 4bytes uint 42
    public byte[] Expiration_Date;      //身份有效日期 4bytes uint 46
    public byte[] Reset_Dat;            //重置日期 4bytes uint 50
    public byte[] Limit_Counts;         //使用上限 (BCD) 4bytes 54
    public byte LastTransferCode;     //前次轉乘代碼 1byte 58
    public byte CurrentTransferCode;  //本次轉乘代碼 1byte 59
    public byte[] TransferTime;         //轉乘日期 4bytes uint 60
    public byte TransferSysID;        //轉乘交易系統編號 1byte 64
    public byte TransferSpID;         //轉乘業者代碼 1byte 65
    public byte TransferTxnType;      //轉乘交易類別 1byte 66
    public byte[] TransferDiscount;     //轉乘優惠金額 2bytes ushort 67
    public byte[] TransferStationID;    //轉乘場站代碼 2bytes ushort 69
    public byte[] TransferDeviceID;     //轉乘設備編號 4bytes uint 71
                                        //===========================================================================================
    public byte ZoneTxnSysID;         //前次段次交易系統編號 1byte 75
    public byte ZoneCode;             //前次段碼 1byte 76
    public byte[] ZoneTxnTime;          //前次段次交易時間 4bytes uint 77
    public byte[] ZoneRouteNo;          //前次段次路線編號 2bytes ushort 81
    public byte[] ZoneTxnCTSN;          //前次段次票卡交易序號 4bytes uint 83
    public byte[] ZoneTxnAmt;           //前次段次交易金額 2bytes ushort 87
    public byte ZoneEntryStatus;      //前次上下車狀態(0x01 -> 上車,0x00 -> 下車) 1byte 89
    public byte ZoneTxnType;          //前次段次交易類別 1byte 90
    public byte ZoneDirection;        //前次往返程註記(0x01 -> 去程,0x02 -> 返程,0x00 -> 循環) 1byte 91
    public byte ZoneSpID;             //前次段次交易業者代號 1byte 92
    public byte[] ZoneStationID;        //前次段次交易場站代碼 2byte 93
    public byte[] ZoneDeviceID;         //前次段次設備編號 4bytes uint 95
                                        //===========================================================================================
    public byte MileTxnSysID;         //前次里程交易系統編號 1byte 99
    public byte[] MileTxnTime;          //前次里程交易時間 4bytes uint 100
    public byte[] MileRouteNo;          //前次里程路線編號 2bytes ushort 104
    public byte[] MileTxnCTSN;          //前次里程票卡交易序號 4bytes uint 106
    public byte[] MileTxnAmt;           //前次里程交易金額 2bytes ushort 110
    public byte MileEntryStatus;      //前次上下車狀態(0x01 -> 上車,0x00 -> 下車) 1byte 112
    public byte MileTxnType;          //前次里程交易類別 1byte 113
    public byte MileTxnMode;          //前次里程交易模式 1byte 114
    public byte MileDirection;        //前次往返程註記(0x01 -> 去程,0x02 -> 返程,0x00 -> 循環) 1byte 115
    public byte MileSpID;             //前次里程交易業者代號 1byte 116
    public byte[] MileStationID;        //前次里程交易場站代碼 2bytes ushort 117
    public byte[] MileDeviceID;         //前次里程設備編號 4bytes uint 119
    public byte[] Receivables;          //上車站到終點站票價 2bytes ushort 123
    public byte RideCounts;           //搭乘次數 1byte 125
    public byte[] RideDate;             //搭乘日期 4bytes uint 126-129
}
struct ICASH_Txns_Mile   //愛金卡寫卡結構體
{
    public byte Trans_Mode;               //交易模式 1byte
    public byte Trans_Type;               //交易類別 1byte
    public byte[] DateTime;                  //交易時間 4bytes uint
    public byte[] Amount;                  //交易金額/點數 2bytes ushort
    public byte[] Receivables;             //上車站到終點站票價 2bytes ushort
    public byte Direction;                //行駛方向 1byte
    public byte Trans_Status;             //交易狀態 1byte
    public byte[] RouteNo;                 //路線編號 2bytes ushort
    public byte[] Current_Station;         //上/下車站別 2bytes ushort
    public byte TransferGroupCode_Before; //前次轉乘群組碼 1byte
    public byte TransferGroupCode;        //本次轉乘群組碼 1byte
    public byte[] TransferDiscount;        //轉乘優惠金額 2bytes ushort
    public byte SysID;                    //交易系統編號 1byte
    public byte CompanyID;                //交易業者編號 1byte
    public byte[] DeviceID;                  //設備編號 4bytes uint
    public byte[] Vehicles_type;           //乘車類別 2bytes ushort
    public byte[] Channel_Type;          //通路識別代碼 3bytes
    public byte[] SocialPntUsed;           //累積已使用社福優惠點數 2bytes ushort
    public byte[] SocialDiscount;          //社福優惠金額 2bytes ushort
    public byte[] SocialResetDate;           //社福優惠點數重置日期 4bytes uint
    public byte RideCounts;               //搭乘次數 1byte
    public byte[] RideDate;                  //搭乘日期 4bytes uint
    public byte[] TradePoint;              //交易點數 2bytes ushort
}
struct DS_CSC_READ_FOR_MILAGE_BV_7B   //悠遊卡讀卡結構體
{
    public byte[] cmd_manufacture_serial_number;             //卡號 uint32 4bytes 5
    public byte cid_issuer_code;                           //發卡公司 uchar 1byte 9
    public byte[] cid_begin_time;                            //票卡起始日期 uint32 4bytes 10(unix)
    public byte[] cid_expiry_time;                           //票卡到期日期 uint32 4bytes 14(unix)
    public byte cid_status;                                //票卡狀態 uchar 1byte 18
    public byte gsp_autopay_flag;                          //自動加值授權認證旗標 uchar 1byte 19
    public byte[] gsp_autopay_value;                         //自動加值金額 ushort 2bytes 20
    public byte[] gsp_max_ev;                                //票卡最大額度上限 ushort 2bytes 22
    public byte[] gsp_max_deduct_value;                      //票卡最大扣款金額 ushort 2bytes 24
    public byte gsp_personal_profile;                      //票卡身份別 uchar 1byte 26
    public byte[] gsp_profile_expiry_date;                   //身份到期日 ushort 2bytes 27(DOS)
    public byte gsp_area_code;                             //區碼 uchar 1byte 29
    public byte gsp_bank_code;                             //銀行代碼 uchar 1byte 30
    public byte area_auth_flag;                            //地區認證旗標 uchar 1byte 31
    public byte special_ticket_type;                       //特殊票票別 uchar 1byte 32
    public byte[] cpd_social_security_code;                  //身份證號碼 uchar 6bytes 33
    public byte[] cpd_deposit;                               //押金 ushort 2bytes 39
    public byte[] ev;                                        //電子錢包額度(餘額) short 2bytes 41
    public byte[] tsd_transaction_sequence_number;           //交易序號 ushort 2bytes 43
    public byte[] tsd_loyalty_points;                        //忠誠累積點數 ushort 2bytes 45
    public byte[] tsd_add_value_accumulated_points;          //加值累積點數 ushort 2bytes 47

    public byte urt_transaction_sequence_number_LSB;       //轉乘交易序號末碼 uchar 1byte 49
    public byte[] urt_transfer_group_code_new;               //新轉乘群組代碼 uchar 2bytes 50
    public byte[] urt_transaction_date_and_time;             //轉乘交易時間 uint32 4bytes 52(unix)
    public byte urt_transaction_type;                      //轉乘方式 char 1byte 56
    public byte[] urt_transfer_discount;                     //轉乘金額 ushort 2bytes 57
    public byte[] urt_ev_afetr_transfer;                     //轉乘後票卡餘額 short 2bytes 59
    public byte urt_transfer_group_code;                   //轉乘群組代碼 uchar 1byte 61
    public byte urt_transaction_location_code;             //轉乘交易場站代碼 uchar 1byte 62
    public byte[] urt_transaction_equipment_id;              //轉乘交易設備編號 uchar 4byte 63

    public byte busfix_fare_product_company;               //特種票交易公司代碼 uchar 1byte 67
    public byte busfix_fare_product_kind;                  //特種票分類 uchar 1byte 68
    public byte busfix_fare_product_type;                  //特種票票種 uchar 1byte 69
    public byte[] busfix_first_possible_utilization_date;    //特種票起始日 ushort 2bytes 70(DOS)
    public byte[] busfix_last_possible_utilization_date;     //特種票到期日 ushort 2bytes 72(DOS)
    public byte busfix_number_of_use;                      //特種票可使用次數 uchar 1byte 74
    public byte busfix_duration_of_use;                    //特種票期限 uchar 1bytes 75
    public byte busfix_authorized_lines;                   //特種票可用路線代碼 uchar 1byte 76
    public byte busfix_authorized_groups;                  //特種票可用路線群組 uchar 1byte 77
    public byte busfix_stop1_number;                       //特種票起迄站代碼 1 uchar 1byte 78
    public byte busfix_stop2_number;                       //特種票起迄站代碼 2 uchar 1byte 79
    public byte[] busfix_vip_points;                         //VIP 票累積儲值點數 ushort 2bytes 80

    public byte busvar_current_used_number;                //特種票已用次數 uchar 1byte 82
    public byte[] busvar_date_of_first_transaction;          //特種票首次交易日期 ushort 2bytes 83(DOS)
    public byte busvar_milage_forbiddance_flag;            //里程計費系統禁用旗標 uchar 1byte 85
    public byte busvar_special_permission;                 //特許權 uchar 1byte 86
    public byte busvar_travel_to_or_from;                  //往返程註記 uchar 1byte 87
    public byte busvar_get_on_or_off;                      //上下車狀態 uchar 1byte 88
    public byte busvar_company_number;                     //交易公司代碼 uchar 1byte 89
    public byte[] busvar_device_serial_number;               //設備次編號 ushort 2bytes 90
    public byte busvar_line_number;                        //路線代碼 uchar 1byte 92
    public byte[] busvar_transaction_date_and_time;          //交易時間 uint32 4bytes 93(unix)
    public byte busvar_stop_number;                        //站牌代碼 uchar 1byte 97
    public byte[] busvar_value_of_transaction;               //交易金額 ushort 2bytes 98
    public byte busvar_travel_mode;                        //搭乘模式 uchar 1byte 100
    public byte busvar_vip_accumulated_points1;            //搭乘次數旗標 &VIP 票累積已用點數 uchar 1byte 101
    public byte busvar_vip_accumulated_points2;            //VIP 票累積已用點數_2 uchar 1byte 102
    public byte busvar_accumulated_free_rides;             //累積優惠點數 uchar 1byte 103
    public byte[] busvar_free_transaction_date_and_time;     //優惠點數交易時間 uint32 4bytes 104(unix)
    public byte[] busvar_accumulated_free_rides2;            //累積優惠點數 2 short 2bytes 108
    public byte[] busvar_free_transaction_date_and_time2;    //累積優惠點數 2 交易日期 char 2bytes 110(DOS)

    public byte var_transaction_number_lsb;                //交易序號末碼 uchar 1byte 112
    public byte[] var_transaction_date_time;                 //加值時間 uint32 4bytes 113(unix)
    public byte var_transaction_type;                      //加值方式代碼 uchar 1byte 117
    public byte[] var_value_of_transaction;                  //加值金額 short 2bytes 118
    public byte[] var_ev_afetr_transaction;                  //加值後餘額 short 2bytes 120
    public byte var_operator_code;                         //交易公司代碼 uchar 1byte 122
    public byte var_transaction_location_code;             //交易場站代碼 uchar 1byte 123
    public byte[] var_transaction_device_id;                 //交易設備編號 uchar 4bytes 124
}
struct DS_CSC_MILAGE_DEDUCT_IN_V3    //悠遊卡寫卡結構體
{
    public byte message_type;                      //訊息代碼 uchar 1byte
    public byte[] utr_transaction_date_time;         //交易時間 uint 4bytes
    public byte[] cmd_manufacture_serial_number;     //卡號 byte 4bytes
    public byte[] deducted_value;                    //扣值金額 ushort 2bytes

    public byte[] urt_transfer_date_time;            //轉乘交易時間 uint 4bytes
    public byte[] urt_transfer_discount;             //轉乘優惠金額 ushort 2bytes
    public byte urt_transfer_group_code;           //轉乘群組代碼 uchar 1byte
    public byte[] urt_transfer_group_code_new;       //新轉乘群組代碼 uchar 2bytes

    public byte busvar_current_used_number;        //特種票已用次數 uchar 1byte
    public byte[] busvar_date_of_first_transaction;  //特種票首次交易日期 ushort 2bytes
    public byte busvar_milage_forbiddance_flag;    //里程計費系統禁用旗標 uchar 1byte
    public byte busvar_special_permission;         //特許權 uchar 1byte
    public byte busvar_travel_to_or_from;          //往返程註記 uchar 1byte
    public byte busvar_get_on_or_off;              //上下車狀態 uchar 1byte
    public byte busvar_line1_number;               //路線代碼 uchar 1byte
    public byte[] busvar_line2_number;               //路線代碼2 uchar 2bytes
    public byte busvar_stop_number;                //站牌代碼 uchar 1byte
    public byte busvar_travel_mode;                //搭乘模式 uchar 1byte
    public byte busvar_transaction_type;           //交易方式代碼 uchar 1byte
    public byte busvar_vip_accumulated_points1;    //當日搭乘次數 &VIP 票累積已用點數1 uchar 1byte
    public byte busvar_vip_accumulated_points2;    //VIP 票累積已用點數2 uchar 1byte
    public byte busvar_remaining_rides;            //優惠剩餘使用次數 uchar 1byte
}

namespace test
{
    public partial class Form1 : Form
    {
        public Form2 F2 = new Form2();
        public Form3 F3 = new Form3();
        public ipass ipass = new ipass();
        public icash icash = new icash();
        public EasyCard EasyCard = new EasyCard();

        Stopwatch Watch = new Stopwatch();
        Queue<byte> RecievedQ = new Queue<byte>();

        
        int StandardTimes = 5, Times,  REC_count = 7, ReaderStatus = 4, TestSuccess = 0, TestFail = 0, ReaderLineCount = 0;
        int ReaderFramewareVersion = 0;
        bool connect = false, start = false, mode = false, REC = false, SearchCard_BT = true,SAMSlotTest_BT=false;
        bool SAMExSlot_Boolen = false;
        string ReaderApVersion, SensingPosition;
        int TestBtnCount=0;
        int SensingTestCount = 0, SensingTestMiddleCount =25, SensingTestFinalCount=50;
        int ThickChange = 1;
        
        /* 尋讀寫時間*/
        long FindTime, ReadTime, WriteTime, AFindTime, AReadTime, AWriteTime;

        /* 歷史最快最慢時間*/
        long HFTTime = 0, HSTTime = 0;

        //容許值時間
        public long[] AllowTime_IPass = { 320, 1000, 200, 1000, 115, 1000 };
        public long[] AllowTime_ICash = { 115, 1000, 315, 1000, 390, 1000 };
        public long[] AllowTime_EasyCard = { 230, 1000, 385, 1000, 165, 1000 };
        public long[] AllowTime_YHDP = { 230, 1000, 385, 1000, 165, 1000 };//遠鑫未檢測

        int[] SAMSlotStatus = {0,0,0,0,0};
        byte[] CardID = new byte[7];
        string[] TestResult = { "None", "None", "None", "None", "None", "None" };

        public enum NowCardTestType
        { 
            未知,
            IPass,
            ICash
        }

        public Form1()
        {
            InitializeComponent();
            ipass.initialization();
            icash.initialization();
            EasyCard.initialization();

            ReaderStatus = 4;

            自動測試ToolStripMenuItem.Checked = true;
            eCCToolStripMenuItem.Checked = true;

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)//限制只能輸入數字
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            if (start) { richTextBox1.Text="天線測試執行中\n\n"; }
            else
            {
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            }



        }

        private void RichTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (start) { richTextBox2.Text=""; }
            else
            {
                richTextBox2.SelectionStart = richTextBox1.TextLength;
                richTextBox2.ScrollToCaret();
            }
        }

        private void Start_BT_Click(object sender, EventArgs e)
        {
            TestResult[2] = "X";
            if (Start_BT.Focused) 
            {
                Times = StandardTimes;
                Start_BT.Enabled = false;
                TestBtnCount++;
                SensingPosition = "Center";

            }
            else if (Start_BT_Forward.Focused)
            {
                Start_BT_Forward.Enabled = false;
                TestBtnCount++;
                SensingPosition = "Forward";



            }
            else if(Start_BT_Back.Focused)
            {
                Start_BT_Back.Enabled = false;
                TestBtnCount++;
                SensingPosition = "Back";

                
            }
            else if(Start_BT_Left.Focused)
            {
                Start_BT_Left.Enabled = false;
                TestBtnCount++;
                SensingPosition = "Left";


            }
            else if(Start_BT_Right.Focused)
            {
                Start_BT_Right.Enabled = false;
                TestBtnCount++;
                SensingPosition = "Right";
              
            }


            if (Times <= 0)
            {
                MessageBox.Show(this, "請將測試次數設為正整數", "開始測試失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (start == false)
                {                    
                    //Start_BT.Text = "停止";
                    start = true;
                }
                else
                {
                    //Start_BT.Text = "天線板測試";
                    start = false;
                }


                ReaderStatus = 4;
                EnQTRAN(4);
            }

        }

        private void OReset_BT_Click(object sender, EventArgs e)
        {
            OFTime_Lab.Text = "0";
            ORTime_Lab.Text = "0";
            OWTime_Lab.Text = "0";
            OTTime_Lab.Text = "0";
        }

        private void AReset_BT_Click(object sender, EventArgs e)
        {
            AFTime_Lab.Text = "0";
            ARTime_Lab.Text = "0";
            AWTime_Lab.Text = "0";
            ATTime_Lab.Text = "0";
            TestFailLab.Text = "0";
            TestSuccessLab.Text = "0";
            TestFail = 0;
            TestSuccess = 0;
            AFindTime = 0;
            AReadTime = 0;
            AWriteTime = 0;
            SensingTestCount = 0;

            /*歷史快慢重置*/
            HFTTime = 0;
            HSTTime = 0;
            HFFTime_Lab.Text = "0";
            HFRTime_Lab.Text = "0";
            HFWTime_Lab.Text = "0";
            HFTTime_Lab.Text = "0";
            HSFTime_Lab.Text = "0";
            HSRTime_Lab.Text = "0";
            HSWTime_Lab.Text = "0";
            HSTTime_Lab.Text = "0";
        }

        private void Clear_BT_Click(object sender, EventArgs e)
        {

            richTextBox1.Text = "";
            richTextBox2.Text = "";
        }

        private void OTotal_Lab_Click(object sender, EventArgs e)
        {

        }

        private void ComPort_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (connect == false)
            {
                try
                {
                    if (RS232.IsOpen != true)
                    {
                        RS232.Open();
                    }

                    //ComPort_CB.Enabled = false;
                    connect = true;
                    ReaderReset.Enabled = true;
                    timer1.Enabled = true;
                    REC = false;
                    REC_count = 7;
                    RecievedQ.Clear();                    
                    richTextBox1.Text += "ComPort連接成功" + Environment.NewLine;
                    richTextBox1.Text += "請輸入讀卡機ID,並按下Enter" + Environment.NewLine;
                }
                catch
                {
                    MessageBox.Show(this, "無法開啟通訊埠. 請確認是否被其它應用程式開啟, 或是通訊埠無效.", "Serial Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                RS232.Close();
                ComPort_CB.Enabled = true;
                connect = false;
                //Start_BT.Text = "天線板測試";
                start = false;
                timer1.Enabled = false;
                TimeOut.Enabled = false;
            }
            Array.Clear(CardID, 0, CardID.Length);
            ReaderStatus = 4;
        }

        private void ReaderReset_Click(object sender, EventArgs e)
        {

            EnQTRAN(3);
            ComPort_CB.Items.Clear();
            Start_BT.Enabled = false;
            SAMSlotTest_Btn.Enabled = false;
            timer1.Enabled = false;
            TimeOut.Enabled = false;
            connect = false;
            richTextBox1.Text += "卡機重啟指令已傳送\n請重選連接埠" + Environment.NewLine;
            richTextBox1.Text += strValue + Environment.NewLine;

        }

        private void SAMSlotTest_Btn_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (SAMSlotTest_Btn.Focused)
            {
                SAMExSlot_Boolen = false; TestResult[0] = "X";


            }
            else if (SAMExSlotTest_Btn.Focused)
            {
                SAMExSlot_Boolen = true; TestResult[1] = "X";
            } 

            SensingTestCount = 0;
            SAMSlotTest_BT = true;
            EnQTRAN(4);

        }

        private void 查卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchCard_BT = false;
            Form2 F2 = new Form2();
            ipass.F2 = F2;
            icash.F2 = F2;
            EasyCard.F2 = F2;
            F2.F1 = this;
            F2.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Baud115200.Checked = true;
            Baud57600.Checked = false;
            eCCSIS2115200ToolStripMenuItem.Checked = false;
            eCCToolStripMenuItem.Checked = false;
            RS232.BaudRate = 115200;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Baud115200.Checked = false;
            Baud57600.Checked = true;
            eCCSIS2115200ToolStripMenuItem.Checked = false;
            eCCToolStripMenuItem.Checked = false;
            RS232.BaudRate = 115200;
        }
        private void eCCSIS2115200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Baud115200.Checked = false;
            Baud57600.Checked = false;
            eCCSIS2115200ToolStripMenuItem.Checked = true;
            eCCToolStripMenuItem.Checked =false;
            RS232.BaudRate = 115200;
        }

        private void DVID_TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                if (mode == false && connect &&DVID_TB.Text!="")
                {                  
                    UI_ini();
                    richTextBox1.Text += "請按下SAM底層槽測試鈕進行測試" + Environment.NewLine;
                    SAMSlotTest_Btn.Enabled= true;                  
                }
            }
        }

        private void eCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Baud115200.Checked = false;
            Baud57600.Checked = false;
            eCCSIS2115200ToolStripMenuItem.Checked = false;
            eCCToolStripMenuItem.Checked = true;
            RS232.BaudRate = 57600;
        }

        private void 自動測試ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connect == true)
            {
                Start_BT.Enabled = true;
            }
            自動測試ToolStripMenuItem.Checked = true;
            靠卡測試ToolStripMenuItem.Checked = false;
            查卡ToolStripMenuItem.Enabled = false;
            SearchCard_BT = true;
            ModeLab.Text = "自動測試模式";
            mode = false;
            Start_BT.Enabled = false;
            Start_BT_Forward.Enabled = false;
            Start_BT_Back.Enabled = false;
            Start_BT_Left.Enabled = false;
            Start_BT_Right.Enabled = false;
            SAMSlotTest_Btn.Enabled = false;
            SAMExSlotTest_Btn.Enabled = false;
            Array.Clear(CardID, 0, CardID.Length);
            ReaderStatus = 4;//讀取卡機版本
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThickChange = 3;
        }

        private void 靠卡測試ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start_BT.Enabled = false;
            //Start_BT.Text = "開始測試";
            ModeLab.Text = "靠卡測試模式";
            自動測試ToolStripMenuItem.Checked = false;
            靠卡測試ToolStripMenuItem.Checked = true;
            查卡ToolStripMenuItem.Enabled = true;

            mode = true;
            Start_BT.Enabled = true;

            Array.Clear(CardID, 0, CardID.Length);            
            ReaderStatus = 4;//讀取卡機版本
            EnQTRAN(4);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text += "請設定COM Port" + Environment.NewLine;
            try
            {
                string path = "TestFile.ini";
                using (StreamReader sr = File.OpenText(path))
                {
                    {
                        long[] AllowTime_IPass = new long[6];
                        long[] AllowTime_EasyCard = new long[6];
                        long[] AllowTime_ICash = new long[6];
                        string line;
                        // Read and display lines from the file until the end of
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            ReaderLineCount++;
                            string[] sArray = Regex.Split(line, ",");
                            if (ReaderLineCount == 1)
                            {
                                StandardTimes = Int32.Parse(sArray[1]);
                                Times = StandardTimes;
                            }
                            if (ReaderLineCount == 2)
                            { continue; }
                            if (ReaderLineCount == 3) //IPass尋讀寫容許值
                            {
                                for (int i = 1; i < 7; i++)
                                {
                                    AllowTime_IPass[i - 1] = Int64.Parse(sArray[i]);

                                }
                            }
                            if (ReaderLineCount == 4)//EasyCard尋讀寫容許值
                            {
                                for (int i = 1; i < 7; i++)
                                {
                                    AllowTime_EasyCard[i - 1] = Int64.Parse(sArray[i]);
                                }
                            }
                            if (ReaderLineCount == 5)//ICash尋讀寫容許值
                            {
                                for (int i = 1; i < 7; i++)
                                {
                                    AllowTime_ICash[i - 1] = Int64.Parse(sArray[i]);
                                }
                            }

                        }
                    }
                }
            }
            catch
            {
                richTextBox1.Text += "參數檔未開啟成功\n已產生預設參數檔" + Environment.NewLine;
                string path = "TestFile.ini";

                if (!File.Exists(path))
                {
                    // Create the file.
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine("TestTimesSet,{0}", Convert.ToString(StandardTimes));
                        sw.WriteLine("CardType,{0},{1},{2},{3},{4},{5}"
                            , "AllowFindTime_IPass", "AllowFindTimeRange_IPass"
                            , "AllowReadTime_IPass", "AllowReadTimeRange_IPass"
                            , "AllowWriteTime_IPass", "AllowWriteTimeRange_IPass"
                            );
                        sw.WriteLine("IPass,{0},{1},{2},{3},{4},{5}"
                            , Convert.ToString(AllowTime_IPass[0]), Convert.ToString(AllowTime_IPass[1])
                            , Convert.ToString(AllowTime_IPass[2]), Convert.ToString(AllowTime_IPass[3])
                            , Convert.ToString(AllowTime_IPass[4]), Convert.ToString(AllowTime_IPass[5])
                            );
                        sw.WriteLine("EasyCard,{0},{1},{2},{3},{4},{5}"
                            , Convert.ToString(AllowTime_EasyCard[0]), Convert.ToString(AllowTime_EasyCard[1])
                            , Convert.ToString(AllowTime_EasyCard[2]), Convert.ToString(AllowTime_EasyCard[3])
                            , Convert.ToString(AllowTime_EasyCard[4]), Convert.ToString(AllowTime_EasyCard[5])
                            );
                        sw.WriteLine("ICash,{0},{1},{2},{3},{4},{5}"
                            , Convert.ToString(AllowTime_ICash[0]), Convert.ToString(AllowTime_ICash[1])
                            , Convert.ToString(AllowTime_ICash[2]), Convert.ToString(AllowTime_ICash[3])
                            , Convert.ToString(AllowTime_ICash[4]), Convert.ToString(AllowTime_ICash[5])
                            );
                        sw.WriteLine("YHDP,{0},{1},{2},{3},{4},{5}"
                            , Convert.ToString(AllowTime_YHDP[0]), Convert.ToString(AllowTime_YHDP[1])
                            , Convert.ToString(AllowTime_YHDP[2]), Convert.ToString(AllowTime_YHDP[3])
                            , Convert.ToString(AllowTime_YHDP[4]), Convert.ToString(AllowTime_YHDP[5])
                            );
                    }
                    string line;
                    using (StreamReader sr = File.OpenText(path))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            ReaderLineCount++;
                            string[] sArray = Regex.Split(line, ",");
                            if (ReaderLineCount == 1)
                            {
                                StandardTimes = Int32.Parse(sArray[1]);
                                Times = StandardTimes;
                            }
                            if (ReaderLineCount == 2)
                            { continue; }
                            if (ReaderLineCount == 3) //IPass尋讀寫容許值
                            {
                                for (int i = 1; i < 7; i++)
                                {
                                    AllowTime_IPass[i - 1] = Int64.Parse(sArray[i]);

                                }
                            }
                            if (ReaderLineCount == 4)//EasyCard尋讀寫容許值
                            {
                                for (int i = 1; i < 7; i++)
                                {
                                    AllowTime_EasyCard[i - 1] = Int64.Parse(sArray[i]);
                                }
                            }
                            if (ReaderLineCount == 5)//ICash尋讀寫容許值
                            {
                                for (int i = 1; i < 7; i++)
                                {
                                    AllowTime_ICash[i - 1] = Int64.Parse(sArray[i]);
                                }
                            }
                            if (ReaderLineCount == 6)//ICash尋讀寫容許值
                            {
                                for (int i = 1; i < 7; i++)
                                {
                                    AllowTime_YHDP[i - 1] = Int64.Parse(sArray[i]);
                                }
                            }

                        }
                    }


                }


            }
        }

        private string strValue;//子視窗回傳值
        public string MsgFromChild
        {
            set { strValue = value; }
        }


        private void 選項ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 F3 = new Form3();
            F3.Owner = this;
            F3.Show();
        }

        private void ComPort_CB_DropDown(object sender, EventArgs e)
        {
            ComPort_CB.Items.Clear();
            ComPort_CB.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            RS232.Close();
        }

        private void ComPort_CB_TextChanged(object sender, EventArgs e)
        {
            if (ComPort_CB.Text != "")
            {
                RS232.PortName = ComPort_CB.Text;
            }
        }
        private void TimeOut_Tick(object sender, EventArgs e)
        {
            ReaderStatus = 4;
            REC_count = 7;
            RecievedQ.Clear();
            if (SearchCard_BT == true)
            {
                richTextBox1.Text += "Timeout" + Environment.NewLine;
            }
            else
            {
                F2.richTextBox1.Text += "Timeout" + Environment.NewLine;
            }
            //Start_BT.Text = "天線板測試";
            start = false;
            TimeOut.Enabled = false;
            if (!mode)
            {
                ReaderStatus = 5;                
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (connect == true)
            {
                if (RS232.IsOpen == false)
                {
                    ComPort_CB.Enabled = true;
                    ComPort_CB.Text = "";
                    connect = false;
                    //Start_BT.Text = "開始";
                    Start_BT.Enabled = false;
                    start = false;
                }
            }


            if (RS232.IsOpen)
            {

                Reader();
                while (RS232.BytesToRead > 0)
                {
                    DataRecieved();
                }
            }
            else
            {
                ReaderStatus = 4;
            }
        }

        private void DataRecieved()
        {
            RecievedQ.Enqueue(Convert.ToByte(RS232.ReadByte()));
            if (RecievedQ.Count == 4)
            {
                REC_count += 256 * RecievedQ.Last();
            }
            if (RecievedQ.Count == 5)
            {
                REC_count += RecievedQ.Last();
            }
            if (REC_count == RecievedQ.Count)
            {
                TimeOut.Enabled = false;
                REC_count = 7;
                REC = true;
            }
        }

        private void EnQTRAN(int Code)  //傳送指令給讀卡機
        {
            switch (Code)
            {
                case -2://PPR_Reset
                    {
                        /*
                        byte[] TM_Location_ID = { 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30 };
                        byte[] TM_ID = { 0x30, 0x30 };
                        byte[] TM_TXN_Date_Time = { 0x32,0x30,0x32,0x30,//年
                                                    0x31,0x30,0x30,0x35,//月日
                                                    0x30,0x39,0x31,0x35,0x30,0x30};//時分秒 
                        byte[] TM_Serial_Number = { 0x30, 0x30, 0x30, 0x30, 0x30, 0x31 };
                        byte[] TM_Agent_Number = { 0x30, 0x30, 0x30, 0x30 };
                        byte[] TXN_Date_Time = { 0x33, 0x70, 0x5E, 0x5E };
                        byte Location_ID =0x65;
                        byte[] New_Location_ID = { 0x65, 0x00 };
                        byte Service_Provider_ID = 0x23;
                        byte[] New_Service_Provider_ID ={ 0x23, 0x00, 0x00 };
                        byte MicroPaymentFlag = 0x80;
                        byte[] OneDayQuotaMicroPayment = { 0x00, 0x00 };
                        byte[] OneceQuotaMicroPayment = { 0x00, 0x00 };
                        byte SAM_Slot_Control_Flag = 0x11;
                        byte MifareKeySet =0x00;
                        byte[] ReservedForUse = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                        byte DataTail = 0xFA;
                        */
                        byte[] PRR_Reset = { 0xEA, 0x04, 0x01, 0x00, 0x47, 0x80, 0x01, 0x00, 0x01, 0x40,
                            0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
                            0x30, 0x30,
                            0x32, 0x30, 0x32, 0x30, 0x30, 0x33, 0x30, 0x33, 0x31, 0x34, 0x35, 0x36, 0x35, 0x31,
                            0x30, 0x30, 0x30, 0x30, 0x30, 0x31,
                            0x30, 0x30, 0x30, 0x30,
                            0x33, 0x70, 0x5E, 0x5E,
                            0x65,
                            0x65, 0x00,
                            0x23,
                            0x23, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x00,
                            0x11,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFA,
                            0xEB, 0x90, 0x00 };
                        
                        int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
                        byte[] timeStampArray=BitConverter.GetBytes(unixTime);
                        string datestr;
                        DateTime gtm = (new DateTime(1970, 1, 1)).AddSeconds(Convert.ToInt32(unixTime));
                        datestr = gtm.ToString("yyyyMMddHHmmss");
                        int TMTXNDateTimeIndex = 22;
                        int TXN_Date_TimeIndex = 46;
                        int j = 0;

                        //richTextBox1.Text += BitConverter.ToString(timeStampArray) + Environment.NewLine;

                        foreach (byte a in datestr) 
                        {
                            PRR_Reset[TMTXNDateTimeIndex+j] = a;
                            //richTextBox1.Text += $"{PRR_Reset[TMTXNDateTimeIndex + j]:X}"+"--" + Environment.NewLine;
                                j++;
                        }

                        for (int i=0;i<4;i++)
                        {
                            PRR_Reset[TXN_Date_TimeIndex+i] = timeStampArray[i];
                            //richTextBox1.Text += $"{timeStampArray[i]:X}" + Environment.NewLine;
                        }

                        PRR_Reset[PRR_Reset.Length - 3] = LRCFuntion(PRR_Reset);

                        //richTextBox1.Text += BitConverter.ToString(PRR_Reset) + Environment.NewLine;//輸出指令
                        //richTextBox1.Text += datestr + Environment.NewLine;
                        
                        RS232.Write(PRR_Reset, 0, PRR_Reset.Length);
                        break;
                    }

                case -1:    //PR_Reset
                    {
                        byte Xor = 0xA4;
                        byte[] PR_Reset = { 0xEA, 0x04, 0x01, 0x00, 0x26, 0x84, 0x01, 0x01, 0x00, 0x20 };
                        Array.Resize(ref PR_Reset, PR_Reset.Length + 35);
                        PR_Reset[PR_Reset.Length - 3] = Xor;
                        PR_Reset[PR_Reset.Length - 2] = 0x90;
                        RS232.Write(PR_Reset, 0, PR_Reset.Length);
                        //richTextBox1.Text += BitConverter.ToString(PR_Reset) + Environment.NewLine;//輸出指令
                        break;
                    }
                case 0:     //尋卡
                    {
                        byte[] FindCardCode = { 0xEA, 0x02, 0x01, 0x00, 0x01, 0x00, 0x90, 0x00 };
                        RS232.Write(FindCardCode, 0, FindCardCode.Length);
                        if (mode == false)
                        {
                            //richTextBox1.Text += "\n第" + Convert.ToString(SensingTestCount + 1) + "次測試:";

                        }
                        break;
                    }
                case 2:     //確認讀卡機中已插入SAM卡類別與卡槽
                    {

                        if (ReaderFramewareVersion == 3) //SAM槽測試用AP
                        { 
                            byte[] FindSAMCardCode = { 0xEA, 0x01, 0x01, 0x00, 0x01, 0x08, 0x90, 0x00 };
                            RS232.Write(FindSAMCardCode, 0, FindSAMCardCode.Length);
                        }
                        else 
                        { 
                            byte[] FindSAMCardCode = { 0xEA, 0x01, 0x01, 0x00, 0x01, 0x04, 0x90, 0x00 };
                            RS232.Write(FindSAMCardCode, 0, FindSAMCardCode.Length);
                        }
                        


                        
                        break;
                    }
                case 3:     //將讀卡機重啟
                    {
                        Char[] Resetchars = { 'R', 'E', 'S', 'E', 'T' };
                        byte[] CmdHead = { 0xEA, 0x01, 0x20, 0x00, 0x05 };
                        byte[] CmdTail = { 0x90, 0x00 };
                        int i = CmdHead.Length;
                        Array.Resize(ref CmdHead, CmdHead.Length + Resetchars.Length + CmdTail.Length);
                        foreach (char code in Resetchars)
                        {
                            CmdHead[i] = Convert.ToByte(code);
                            i++;
                        }
                        CmdHead[CmdHead.Length - 2] = 0x90;

                        //richTextBox1.Text += BitConverter.ToString(CmdHead) + Environment.NewLine;//顯示輸出指令
                        RS232.Write(CmdHead, 0, CmdHead.Length);
                        break;
                    }
                case 4:     //讀取卡機AP版本
                    {

                        byte[] GetVersion = { 0xEA, 0x01, 0x00, 0x00, 0x01, 0x00, 0x90, 0x00, };
                        //richTextBox1.Text += BitConverter.ToString(GetVersion) + Environment.NewLine;//顯示輸出指令
                        RS232.Write(GetVersion, 0, GetVersion.Length);
                        break;
                    }
                case 41:    //悠遊卡讀卡
                    {
                        byte[] PR_ReadForMilageBV2 = { 0xEA, 0x04, 0x01, 0x00, 0x06, 0x68, 0x13, 0x01, 0x00, 0x7B, 0x01, 0x90, 0x00 };
                        RS232.Write(PR_ReadForMilageBV2, 0, PR_ReadForMilageBV2.Length);
                        break;
                    }
                case 42:    //悠遊卡寫卡
                    {
                        byte Xor = 0x00;
                        byte[] EasyCardWriteCode = { 0xEA, 0x04, 0x01, 0x00, 0x2B, 0x68, 0x13, 0x03, 0x03, 0x24 };
                        Array.Resize(ref EasyCardWriteCode, 50);
                        Array.Copy(EasyCard.GetWriteStruct(), 0, EasyCardWriteCode, 10, EasyCard.GetWriteStruct().Length);
                        EasyCardWriteCode[EasyCardWriteCode.Length - 4] = 0x21;
                        for (int a = 5; a < EasyCardWriteCode.Length - 3; a++)
                        {
                            Xor ^= EasyCardWriteCode[a];
                        }
                        EasyCardWriteCode[EasyCardWriteCode.Length - 3] = Xor;
                        EasyCardWriteCode[EasyCardWriteCode.Length - 2] = 0x90;
                        RS232.Write(EasyCardWriteCode, 0, EasyCardWriteCode.Length);
                        break;
                    }
                case 51:    //一卡通讀卡
                    {
                        byte Xor = 0x00;
                        byte[] ipassreadCode = { 0xEA, 0x05, 0x01, 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x00 };
                        int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
                        Array.Copy(CardID, 0, ipassreadCode, 5, 4);
                        Array.Copy(BitConverter.GetBytes(unixTime), 0, ipassreadCode, 9, 4);
                        for (int a = 5; a < 13; a++)
                        {
                            Xor ^= ipassreadCode[a];
                        }
                        ipassreadCode[ipassreadCode.Length - 3] = Xor;
                        RS232.Write(ipassreadCode, 0, ipassreadCode.Length);
                        break;
                    }
                case 52:    //一卡通寫卡
                    {
                        if (ReaderFramewareVersion == 1)
                        {
                            byte Xor = 0x00;
                            byte[] ipasswriteCode = { 0xEA, 0x05, 0x00, 0x00, 0x00 };
                            if (ipass.Len <= 128)//無Maas段
                            {
                                Array.Resize(ref ipasswriteCode, 61);
                                ipasswriteCode[4] = 0x36;
                            }
                            else
                            {
                                Array.Resize(ref ipasswriteCode, 105);
                                ipasswriteCode[4] = 0x62;
                            }

                            if (ipass.Get_preBUSStatus() == 0x00)//目前卡片讀卡結果為上或下車狀態
                            {
                                ipasswriteCode[2] = 0x02;
                            }
                            else
                            {
                                ipasswriteCode[2] = 0x03;
                            }

                            Array.Copy(ipass.GetWriteStruct(), 0, ipasswriteCode, 5, ipass.GetWriteStruct().Length);

                            //LRC
                            for (int i = 5; i < ipasswriteCode.Length - 3; i++)
                            {
                                Xor ^= ipasswriteCode[i];
                            }

                            ipasswriteCode[ipasswriteCode.Length - 3] = Xor;
                            ipasswriteCode[ipasswriteCode.Length - 2] = 0x90;

                            //richTextBox1.Text += "長度:" + ipasswriteCode.Length + ";" + BitConverter.ToString(ipasswriteCode) + Environment.NewLine;//輸出指令
                            RS232.Write(ipasswriteCode, 0, ipasswriteCode.Length);
                        }
                        else if (ReaderFramewareVersion == 2 || ReaderFramewareVersion == 3)
                        {
                            byte Xor = 0x00;
                            byte[] ipasswriteCode = { 0xEA, 0x05, 0x00, 0x00, 0x5A };
                            Array.Resize(ref ipasswriteCode, 97);
                            if (ipass.Get_preBUSStatus() == 0x00)//目前卡片讀卡結果為上或下車狀態
                            {
                                ipasswriteCode[2] = 0x02;
                            }
                            else
                            {
                                ipasswriteCode[2] = 0x03;
                            }

                            Array.Copy(ipass.GetWriteStruct(), 0, ipasswriteCode, 5, ipass.GetWriteStruct().Length);

                            //LRC
                            for (int i = 5; i < ipasswriteCode.Length - 3; i++)
                            {
                                Xor ^= ipasswriteCode[i];
                            }

                            ipasswriteCode[ipasswriteCode.Length - 3] = Xor;
                            ipasswriteCode[ipasswriteCode.Length - 2] = 0x90;

                            //richTextBox1.Text += "長度:" + ipasswriteCode.Length + ";" + BitConverter.ToString(ipasswriteCode) + Environment.NewLine;//輸出指令
                            RS232.Write(ipasswriteCode, 0, ipasswriteCode.Length);
                        }
                        
                        
                        break;
                    }
                case 61:    //愛金卡讀卡
                    {
                        byte Xor = 0x00;
                        byte[] icashreadCode = { 0xEA, 0x06, 0x01, 0x00, 0x0D, 0x07 };
                        int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
                        Array.Resize(ref icashreadCode, 20);
                        Array.Copy(CardID, 0, icashreadCode, 6, 7);
                        Array.Copy(BitConverter.GetBytes(unixTime), 0, icashreadCode, 13, 4);
                        for (int a = 5; a < 17; a++)
                        {
                            Xor ^= icashreadCode[a];
                        }
                        icashreadCode[icashreadCode.Length - 3] = Xor;
                        icashreadCode[icashreadCode.Length - 2] = 0x90;
                        RS232.Write(icashreadCode, 0, icashreadCode.Length);
                        break;
                    }
                case 62:    //愛金卡寫卡
                    {
                        byte Xor = 0x00;
                        byte[] icashwriteCode = { 0xEA, 0x06, 0x02, 0x00, 0x2F };
                        Array.Resize(ref icashwriteCode, 54);
                        Array.Copy(icash.GetWriteStruct(), 0, icashwriteCode, 5, icash.GetWriteStruct().Length);
                        for (int a = 5; a < 51; a++)
                        {
                            Xor ^= icashwriteCode[a];
                        }
                        icashwriteCode[icashwriteCode.Length - 3] = Xor;
                        icashwriteCode[icashwriteCode.Length - 2] = 0x90;
                        RS232.Write(icashwriteCode, 0, icashwriteCode.Length);
                        break;
                    }
            }
            Watch.Restart();
            Watch.Start();
            TimeOut.Enabled = true;
        }
        private void VersionCheck(byte[] VersionTitle, byte[] VersionInfo) 
        {
            ReaderFramewareVersion = 0;
            byte[] F2D= { Convert.ToByte('F'), Convert.ToByte('2'), Convert.ToByte('D') };
            ReaderApVersion = Encoding.ASCII.GetString(VersionInfo, 5, VersionInfo.Length-7);
            richTextBox1.Text += Environment.NewLine;
            if (Convert.ToChar(VersionTitle[0]) == 'F' && Convert.ToChar(VersionTitle[1]) == '1' && Convert.ToChar(VersionTitle[2]) == 'M')
            {
                ReaderFramewareVersion = 1;
                richTextBox1.Text += Environment.NewLine+"第一代卡機" + Environment.NewLine;
                
            }
            else if (Convert.ToChar(VersionTitle[0]) == 'B' && Convert.ToChar(VersionTitle[1]) == '1' && Convert.ToChar(VersionTitle[2]) == 'M')
            {
                ReaderFramewareVersion = 2;
                richTextBox1.Text += "第一代卡機" + Environment.NewLine;
            }
            else if (VersionTitle[0] == F2D[0] && VersionTitle[1] == F2D[1] && VersionTitle[2] == F2D[2])
            {
                ReaderFramewareVersion = 2;
                richTextBox1.Text += "第二代卡機" + Environment.NewLine;
            }
            else if (Convert.ToChar(VersionTitle[0]) == 'T' && Convert.ToChar(VersionTitle[1]) == 'S' && Convert.ToChar(VersionTitle[2]) == '2')
            {
                ReaderFramewareVersion = 3;
                richTextBox1.Text += "第二代卡機" + Environment.NewLine;
            }
            else { 
                richTextBox1.Text += "未定義卡機韌體:"+Convert.ToChar(VersionTitle[0])+ Convert.ToChar(VersionTitle[1])+ Convert.ToChar(VersionTitle[2]) + Environment.NewLine; 

            }

        }
        private void TestTimesCount(int TestResult) //測試次數計算
        {
            switch (TestResult)
            {
                case 0:     //TestFail(太慢)
                    if (mode == false)
                    {
                        SensingTestCount++;
                        Times--;
                        TestFail++;
                        TestFailLab.Text = (TestFail).ToString();

                        AFindTime += FindTime;
                        AReadTime += ReadTime;
                        AWriteTime += WriteTime;

                        AFTime_Lab.Text = (AFindTime / SensingTestCount).ToString();
                        ARTime_Lab.Text = (AReadTime / SensingTestCount).ToString();
                        AWTime_Lab.Text = (AWriteTime / SensingTestCount).ToString();
                        ATTime_Lab.Text = ((AFindTime + AReadTime + AWriteTime) / SensingTestCount).ToString();
                    }
                    break;
                case 1:
                    if (mode == false)
                    {
                        SensingTestCount++;
                        Times--;
                        TestSuccess++;

                        TestSuccessLab.Text = (TestSuccess).ToString();

                        AFindTime += FindTime;
                        AReadTime += ReadTime;
                        AWriteTime += WriteTime;

                        AFTime_Lab.Text = (AFindTime / SensingTestCount).ToString();
                        ARTime_Lab.Text = (AReadTime / SensingTestCount).ToString();
                        AWTime_Lab.Text = (AWriteTime / SensingTestCount).ToString();
                        ATTime_Lab.Text = ((AFindTime + AReadTime + AWriteTime) / SensingTestCount).ToString();
                    }
                    break;
            }
        }
        private void SingleTestTimeLableChange()
        {
            OFTime_Lab.Text = FindTime.ToString();
            ORTime_Lab.Text = ReadTime.ToString();
            OWTime_Lab.Text = WriteTime.ToString();
            OTTime_Lab.Text = (FindTime + ReadTime + WriteTime).ToString();
        }

        private void HistoryVelocity()
        {
            if (HFTTime == 0)//初始化歷史快慢值
            {
                HFTTime = FindTime + ReadTime + WriteTime;
                HSTTime = FindTime + ReadTime + WriteTime;
                HFFTime_Lab.Text = OFTime_Lab.Text;
                HFRTime_Lab.Text = ORTime_Lab.Text;
                HFWTime_Lab.Text = OWTime_Lab.Text;
                HFTTime_Lab.Text = OTTime_Lab.Text;
                HSFTime_Lab.Text = OFTime_Lab.Text;
                HSRTime_Lab.Text = ORTime_Lab.Text;
                HSWTime_Lab.Text = OWTime_Lab.Text;
                HSTTime_Lab.Text = OTTime_Lab.Text;
            }
            else if (HFTTime >= (FindTime + ReadTime + WriteTime))
            {
                HFTTime = (FindTime + ReadTime + WriteTime);
                HFFTime_Lab.Text = OFTime_Lab.Text;
                HFRTime_Lab.Text = ORTime_Lab.Text;
                HFWTime_Lab.Text = OWTime_Lab.Text;
                HFTTime_Lab.Text = OTTime_Lab.Text;

            }
            else if (HSTTime <= (FindTime + ReadTime + WriteTime))
            {
                HSTTime = (FindTime + ReadTime + WriteTime);
                HSFTime_Lab.Text = OFTime_Lab.Text;
                HSRTime_Lab.Text = ORTime_Lab.Text;
                HSWTime_Lab.Text = OWTime_Lab.Text;
                HSTTime_Lab.Text = OTTime_Lab.Text;
            }

        }

        private void Reader()   //卡機流程
        {
            switch (ReaderStatus)
            {
                case -1:    //PR_Reset & PPR_Reset
                    {
                        if (REC == true)
                        {
                            byte[] Recieved = RecievedQ.ToArray();
                            RecievedQ.Clear();
                            richTextBox2.Text += "GetPR_Reset:"+BitConverter.ToString(Recieved) + Environment.NewLine;
                            if (ReaderFramewareVersion == 1)
                            {
                                if (Recieved.Length == 9)
                                {
                                    richTextBox1.Text += "PR_Reset成功" + Environment.NewLine;
                                    ReaderStatus = 2;
                                }
                                else
                                {
                                    richTextBox1.Text += "PR_Reset失敗" + Environment.NewLine;
                                    ReaderStatus = 2;
                                }
                            }
                            else if (ReaderFramewareVersion == 2)
                            {
                                if (Recieved.Length == 260)
                                {
                                    richTextBox1.Text += "PPR_Reset成功" + Environment.NewLine;
                                    ReaderStatus = 2;
                                }
                                else
                                {
                                    richTextBox1.Text += "PPR_Reset失敗" + Environment.NewLine;
                                    ReaderStatus = 2;
                                }
                            }


                            REC = false;
                            EnQTRAN(2);
                        }
                        break;
                    }
                case 0:     //尋卡
                    {
                        if (!(mode))
                        {
                            if (Times == 0)
                            {
                                Times = StandardTimes;
                                start = false;
                                //richTextBox1.Text += "天線測試完成\n\n";
                                //Start_BT.Text = "天線板測試";
                            }
                        }

                        if (mode || start)
                        {
                            RecievedQ.Clear();
                            REC_count = 7;
                            REC = false;
                            ReaderStatus = 1;
                            EnQTRAN(0);
                            
                        }
                        break;
                    }
                case 1:     //判斷卡片種類
                    {
                        if (REC == true)
                        {
                            byte[] Recieved = RecievedQ.ToArray();
                            richTextBox2.Text += "卡種:" + BitConverter.ToString(Recieved) + Environment.NewLine;
                            RecievedQ.Clear();
                            if (Recieved.Length == 8 || Recieved.Length == 19)
                            {
                                Watch.Stop();
                                FindTime = Watch.ElapsedMilliseconds;
                                switch (Recieved[5])
                                {
                                    case 0x01:  //無卡
                                        {
                                            //richTextBox1.Text += "無卡" + Environment.NewLine;
                                            TestTimesCount(0);
                                            CreatSensingTestData(Convert.ToInt32(DVID_TB.Text), "Err", SensingTestCount, ThickChange);

                                            if (TestBtnCount % 5 == 0 && SensingTestCount% SensingTestMiddleCount == 0)
                                            {
                                                ThickChange = 5;
                                                start = false;
                                                richTextBox1.Text += $"將替換為{ThickChange}公分治具\n"; 
                                                                                     

                                            }
                                            


                                            if (SensingTestCount == SensingTestFinalCount)//FRW結果 
                                            {
                                                TestResult[2] = "O";
                                                TestResult[3] = $"{(AFindTime + AReadTime + AWriteTime) / SensingTestCount}";
                                                TestResult[4] = $"{HFTTime}";
                                                TestResult[5] = $"{HSTTime}";
                                                ReaderStatus = 5;
                                                break;
                                            }

                                            Array.Clear(CardID, 0, CardID.Length);
                                            ReaderStatus = 0;
                                            TestBtnEnable(SensingTestCount, $"{NowCardTestType.未知}",ThickChange);
                                            break;
                                        }
                                    case 0x02:  //多卡重疊
                                        {
                                            start = false;
                                            richTextBox1.Text += "多卡重疊" + Environment.NewLine;
                                            if (mode)
                                            {
                                                ReaderStatus = 0;//重新測試
                                            }
                                            else 
                                            {
                                                ReaderStatus = 5;//停止測試
                                            }


                                                break;
                                        }
                                    case 0x00:  //未知
                                        {
                                            TestTimesCount(0);
                                            start = false;
                                            richTextBox1.Text += "未知卡片" + Environment.NewLine;
                                            if (mode)
                                            {
                                                ReaderStatus = 0;//重新測試
                                            }
                                            else
                                            {
                                                ReaderStatus = 5;//停止測試
                                            }
                                            break;
                                        }
                                    case 0x03:  //遠鑫卡
                                        {
                                            richTextBox1.Text += "遠鑫卡:" + BitConverter.ToString(Recieved, 6, 4) + Environment.NewLine;
                                            ReaderStatus = 0;
                                            break;
                                        }
                                    case 0x04:  //悠遊卡
                                        {
                                            if (mode == true)
                                            {
                                                if (BitConverter.ToString(Recieved, 6, 4) != BitConverter.ToString(CardID, 0, 4))
                                                {
                                                    Array.Copy(Recieved, 6, CardID, 0, 4);
                                                    if (SearchCard_BT)
                                                    {
                                                        richTextBox1.Text += "悠遊卡 ";
                                                    }
                                                    ReaderStatus = 41;
                                                    EnQTRAN(41);
                                                }
                                                else
                                                {
                                                    ReaderStatus = 0;
                                                }
                                            }
                                            else
                                            {
                                                Array.Copy(Recieved, 6, CardID, 0, 4);
                                                richTextBox1.Text += "悠遊卡 ";
                                                ReaderStatus = 41;
                                                EnQTRAN(41);
                                            }
                                            break;
                                        }
                                    case 0x05:  //一卡通
                                        {
                                            if (mode == true)
                                            {
                                                if (BitConverter.ToString(Recieved, 6, 4) != BitConverter.ToString(CardID, 0, 4))
                                                {
                                                    Array.Copy(Recieved, 6, CardID, 0, 4);
                                                    if (SearchCard_BT)
                                                    {
                                                        richTextBox1.Text += "一卡通 ";
                                                    }
                                                    ReaderStatus = 51;
                                                    EnQTRAN(51);
                                                }
                                                else
                                                {
                                                    ReaderStatus = 0;
                                                }
                                            }
                                            else
                                            {
                                                Array.Copy(Recieved, 6, CardID, 0, 4);
                                                richTextBox1.Text += "一卡通 ";
                                                ReaderStatus = 51;
                                                EnQTRAN(51);
                                            }
                                            break;
                                        }
                                    case 0x06:  //愛金卡
                                        {
                                            if (mode == true)
                                            {
                                                if (BitConverter.ToString(Recieved, 6, 7) != BitConverter.ToString(CardID, 0, 7))
                                                {
                                                    Array.Copy(Recieved, 6, CardID, 0, 7);
                                                    if (SearchCard_BT)
                                                    {
                                                        richTextBox1.Text += "愛金卡 ";
                                                    }
                                                    ReaderStatus = 61;
                                                    EnQTRAN(61);
                                                }
                                                else
                                                {
                                                    ReaderStatus = 0;
                                                }
                                            }
                                            else
                                            {
                                                Array.Copy(Recieved, 6, CardID, 0, 7);
                                                richTextBox1.Text += "愛金卡 ";
                                                ReaderStatus = 61;
                                                EnQTRAN(61);
                                            }
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                start = false;
                                richTextBox1.Text += "尋卡失敗" + Environment.NewLine;
                                ReaderStatus = 5;
                            }
                            REC = false;
                        }
                        break;
                    }

                case 2:    //確認卡機SAM卡種類
                    {
                        if (REC == true)
                        {
                            byte[] Recieved = RecievedQ.ToArray();
                            
                            richTextBox2.Text += "SAM卡槽:" + BitConverter.ToString(Recieved) + Environment.NewLine;
                            if (Recieved.Length == 19)
                            {
                                richTextBox1.Text += "\nSAM槽已置入卡別:" + Environment.NewLine;
                                if (!SAMExSlot_Boolen)
                                {
                                    if (Recieved[5] > 0)
                                    {
                                        richTextBox1.Text += "台智卡可用,SAM" + Convert.ToString(Recieved[5]) + Environment.NewLine;
                                        SAMSlotStatus[0] = Recieved[5];
                                    }
                                    if (Recieved[10] > 0)
                                    {
                                        richTextBox1.Text += "遠鑫卡可用,SAM" + Convert.ToString(Recieved[10]) + Environment.NewLine;
                                        SAMSlotStatus[1] = Recieved[10];
                                    }

                                    if (Recieved[14] > 0)
                                    {
                                        richTextBox1.Text += "悠遊卡可用,SAM" + Convert.ToString(Recieved[14]) + Environment.NewLine;
                                        SAMSlotStatus[2] = Recieved[14];
                                    }
                                    if (Recieved[15] > 0)
                                    {
                                        richTextBox1.Text += "一卡通可用,SAM" + Convert.ToString(Recieved[15]) + Environment.NewLine;
                                        SAMSlotStatus[3] = Recieved[15];
                                    }
                                    if (Recieved[16] > 0)
                                    {
                                        richTextBox1.Text += "愛金卡可用,SAM" + Convert.ToString(Recieved[16]) + Environment.NewLine;
                                        SAMSlotStatus[4] = Recieved[16];
                                    }
                                    int TestResultSucces = 0;
                                    foreach (int SAM in SAMSlotStatus)
                                    {

                                        if (SAM > 0)
                                        {
                                            TestResultSucces++;
                                        }
                                    }
                                    if (TestResultSucces >= 4)
                                    {
                                        TestResult[0] = "O";
                                    }
                                    else
                                    {
                                        CreatSAMTestData(Convert.ToInt32(DVID_TB.Text), SAMSlotStatus);
                                        ReaderStatus = 5;
                                        break;
                                    }

                                }
                                else
                                {
                                    if (Recieved[5] > 0)
                                    {
                                        richTextBox1.Text += "台智卡可用,SAM" + Convert.ToString(Recieved[5] + 4) + Environment.NewLine;
                                        SAMSlotStatus[0] = Recieved[5] + 4;
                                    }
                                    if (Recieved[10] > 0)
                                    {
                                        richTextBox1.Text += "遠鑫卡可用,SAM" + Convert.ToString(Recieved[10] + 4) + Environment.NewLine;
                                        SAMSlotStatus[1] = Recieved[10] + 4;
                                    }

                                    if (Recieved[14] > 0)
                                    {
                                        richTextBox1.Text += "悠遊卡可用,SAM" + Convert.ToString(Recieved[14] + 4) + Environment.NewLine;
                                        SAMSlotStatus[2] = Recieved[14] + 4;
                                    }
                                    if (Recieved[15] > 0)
                                    {
                                        richTextBox1.Text += "一卡通可用,SAM" + Convert.ToString(Recieved[15] + 4) + Environment.NewLine;
                                        SAMSlotStatus[3] = Recieved[15] + 4;
                                    }
                                    if (Recieved[16] > 0)
                                    {
                                        richTextBox1.Text += "愛金卡可用,SAM" + Convert.ToString(Recieved[16] + 4) + Environment.NewLine;
                                        SAMSlotStatus[4] = Recieved[16] + 4;
                                    }
                                    int TestResultSucces = 0;
                                    foreach (int SAM in SAMSlotStatus)
                                    {

                                        if (SAM > 0)
                                        {
                                            TestResultSucces++;
                                        }
                                    }
                                    if (TestResultSucces >= 4)
                                    {
                                        TestResult[1] = "O";
                                    }
                                    else
                                    {
                                        CreatSAMTestData(Convert.ToInt32(DVID_TB.Text), SAMSlotStatus);
                                        ReaderStatus = 5;
                                        break;
                                    }
                                }
                                if (!mode)
                                {
                                    CreatSAMTestData(Convert.ToInt32(DVID_TB.Text), SAMSlotStatus);

                                    RecievedQ.Clear();
                                    ReaderStatus = 4;

                                }

                            }
                            else if (Recieved.Length == 16)
                            {
                                richTextBox1.Text += $"\nSAM槽已置入數量:{Recieved[5]}" + Environment.NewLine;
                                if (!SAMExSlot_Boolen) //底層SAM
                                {
                                    for (int i = 6; i < 10; i++)
                                    {
                                        if (Recieved[i] == 2) { richTextBox1.Text += $"SAM{i - 1}:一卡通\n"; }
                                        else if (Recieved[i] == 3) { richTextBox1.Text += $"SAM{i - 1}:ICash\n"; }
                                        else { richTextBox1.Text += $"SAM{i - 1}:未置入\n"; }

                                    }
                                    if (Recieved[5] < 4)
                                    {                                       
                                        ReaderStatus = 5;//測試結束
                                        break;
                                    }
                                    else 
                                    {
                                        TestResult[0] = "O";
                                        RecievedQ.Clear();
                                        ReaderStatus = 4;

                                           
                                            richTextBox1.Text += "請按下SAM擴充槽測試" + Environment.NewLine;
                                            SAMSlotTest_Btn.Enabled = false;
                                            SAMExSlotTest_Btn.Enabled = true;

                                        

                                    }


                                }
                                else//上層SAM
                                {
                                    

                                    for (int i = 6; i < 10; i++)
                                    {
                                        if (Recieved[i] == 2) { richTextBox1.Text += $"SAM{i - 5}:一卡通\n"; }
                                        else if (Recieved[i] == 3) { richTextBox1.Text += $"SAM{i - 5}:ICash\n"; }
                                        else { richTextBox1.Text += $"SAM{i - 5}:未置入\n"; }

                                    }
                                    if (Recieved[5] < 4)
                                    {                                        
                                        ReaderStatus = 5;
                                        break;
                                    }
                                    else
                                    {
                                        TestResult[1] = "O";
                                        RecievedQ.Clear();
                                        ReaderStatus = 4;


                                            SAMExSlotTest_Btn.Enabled = false;                                           
                                            Start_BT.Enabled = true;
                                        richTextBox1.Text += "\n\n放1公分治具與一卡通(IPass)卡片於中間位置 " + Environment.NewLine;
                                        richTextBox1.Text += "\n\n按下中間位置的天線測試鈕 " + Environment.NewLine;
                                    }
                                }




                            }
                            else
                            {
                                richTextBox1.Text += "SAM槽讀取失敗" + Environment.NewLine;
                                ReaderStatus = 4;
                            }
                            REC = false;
                        }
                        break;
                    }
                case 4:    //天線測試時,卡機韌體版本顯示
                    {
                        if (mode || start)
                        {
                            
                            if (REC == true)
                            {
                                byte[] VersionTitle = new byte[3];
                                byte[] Recieved = RecievedQ.ToArray();
                                int i = 0, j = 0;
                                RecievedQ.Clear();
                                richTextBox2.Text += "Get版本"+BitConverter.ToString(Recieved) + Environment.NewLine;
                                try
                                {
                                    if (Recieved[1] == 0x01 && Recieved[2] == 0x00)
                                    {
                                        //richTextBox1.Text += "版本讀取成功" + Environment.NewLine;
                                        richTextBox1.Text += "版本:";

                                        /*獲取前3版本字*/
                                        foreach (byte words in Recieved)
                                        {
                                            if (i > 4 && i < 8)
                                            {
                                                richTextBox1.Text += Convert.ToChar(words);
                                                VersionTitle[j] = words;
                                                j++;
                                            }
                                            else if (i >= 8 && i < Recieved.Length - 2)
                                            {
                                                richTextBox1.Text += Convert.ToChar(words);
                                            }
                                            i++;
                                        }
                                        VersionCheck(VersionTitle, Recieved);
                                        richTextBox1.Text += Environment.NewLine;
                                        ReaderStatus = 0;
                                    }
                                    else
                                    {
                                        richTextBox1.Text += "版本讀取失敗" + Environment.NewLine;
                                        //Start_BT.Text = "天線板測試";
                                        start = false;
                                        ReaderStatus = 4;
                                    }
                                }
                                catch {
                                    richTextBox1.Text += "版本讀取失敗" + Environment.NewLine;
                                   // Start_BT.Text = "天線板測試";
                                    start = false;
                                    ReaderStatus = 4;
                                }
                               
                                REC = false;
                            }
                        }
                        else if (SAMSlotTest_BT == true)
                        {
                            if (REC == true)
                            {
                                byte[] VersionTitle = new byte[3];
                                byte[] Recieved = RecievedQ.ToArray();
                                int i = 0, j = 0;
                                RecievedQ.Clear();
                                richTextBox2.Text += "版本讀取:"+BitConverter.ToString(Recieved) + Environment.NewLine;
                                try
                                {
                                    if (Recieved[1] == 0x01 && Recieved[2] == 0x00)
                                    {
                                        //richTextBox1.Text += "版本讀取成功" + Environment.NewLine;
                                        richTextBox1.Text += "版本:";

                                        /*獲取前3版本字*/
                                        foreach (byte words in Recieved)
                                        {
                                            if (i > 4 && i < 8)
                                            {
                                                richTextBox1.Text += Convert.ToChar(words);
                                                VersionTitle[j] = words;
                                                j++;
                                            }
                                            else if (i >= 8 && i < Recieved.Length - 2)
                                            {
                                                richTextBox1.Text += Convert.ToChar(words);
                                            }
                                            i++;
                                        }
                                        VersionCheck(VersionTitle, Recieved);
                                        richTextBox1.Text += Environment.NewLine;

                                        if (ReaderFramewareVersion == 1)
                                        {
                                            ReaderStatus = -1;
                                            EnQTRAN(-1);
                                        }
                                        else if (ReaderFramewareVersion == 2)
                                        {
                                            ReaderStatus = -1;
                                            EnQTRAN(-2);
                                        }
                                        else
                                        {
                                            ReaderStatus = 2;
                                            richTextBox1.Text += "跳過PR_Reset";
                                            EnQTRAN(2);
                                        }


                                    }
                                    else
                                    {
                                        richTextBox1.Text += "版本讀取失敗" + Environment.NewLine;
                                      //  Start_BT.Text = "天線板測試";
                                        start = false;
                                        ReaderStatus = 4;
                                    }

                                }
                                catch 
                                {
                                    richTextBox1.Text += "版本讀取失敗" + Environment.NewLine;
                                    //Start_BT.Text = "天線板測試";
                                    start = false;
                                    ReaderStatus = 4;
                                }
                                
                                SAMSlotTest_BT = false;
                                REC = false;
                            }

                        }
                        break;
                    }
                case 5: //自動測試結束進行初始化
                    {

                        CreatTestResultData(Convert.ToInt32(DVID_TB.Text),TestResult);                        
                        richTextBox1.Text += "測試結束,已匯出測試報告" + Environment.NewLine;
                        richTextBox1.Text += $"SAMSlot_Bottom：{TestResult[0]}\n" + $"SAMSlot_Top：{TestResult[1]}\n" + $"尋讀寫結果：{TestResult[2]}" + Environment.NewLine;
                        DVID_TB.Clear();
                        UI_ini();
                        break;
                    }
                case 41:    //悠遊卡讀卡
                    {
                        if (REC == true)
                        {
                            richTextBox2.Text += "悠遊讀卡" + BitConverter.ToString(RecievedQ.ToArray()) + Environment.NewLine;
                            if (RecievedQ.Count == 131)
                            {
                                Watch.Stop();
                                ReadTime = Watch.ElapsedMilliseconds;
                                EasyCard.BytesToReadStruct(RecievedQ.ToArray());
                                RecievedQ.Clear();
                                if (SearchCard_BT == true || mode == false)
                                {
                                    richTextBox1.Text += "餘額:" + EasyCard.Get_ev() + Environment.NewLine;
                                    if (EasyCard.Get_ev() >= 0)
                                    {
                                        ReaderStatus = 42;
                                        EnQTRAN(42);
                                    }
                                    else
                                    {
                                        richTextBox1.Text += "餘額不足" + Environment.NewLine;
                                        ReaderStatus = 0;
                                    }
                                }
                                else
                                {
                                    EasyCard.PrintInfo();
                                    ReaderStatus = 0;
                                }
                            }
                            else
                            {
                                TestTimesCount(0);
                                richTextBox1.Text += "讀卡失敗" + Environment.NewLine;
                                Array.Clear(CardID, 0, CardID.Length);
                                ReaderStatus = 0;
                            }
                            REC = false;
                        }
                        break;
                    }
                case 42:    //悠遊卡寫卡
                    {
                        if (REC == true)
                        {
                            byte[] Recieved = RecievedQ.ToArray();
                            RecievedQ.Clear();
                            richTextBox2.Text += "悠遊寫卡:" + BitConverter.ToString(Recieved) + Environment.NewLine;
                            if (Recieved.Length == 41)
                            {
                                Watch.Stop();
                                WriteTime = Watch.ElapsedMilliseconds;
                                if (Recieved[25] == 0x15)
                                {
                                    richTextBox1.Text += "交易成功(上)" + Environment.NewLine;
                                }
                                else
                                {
                                    richTextBox1.Text += "交易成功(下)" + Environment.NewLine;
                                }
                                SingleTestTimeLableChange();
                                HistoryVelocity();

                                if (mode == false)
                                {
                                    if (FindTime >= AllowTime_EasyCard[0] + AllowTime_EasyCard[1])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "悠遊卡尋卡超時" + Environment.NewLine;
                                    }
                                    else if (ReadTime >= AllowTime_EasyCard[2] + AllowTime_EasyCard[3])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "悠遊卡讀卡超時" + Environment.NewLine;
                                    }
                                    else if (WriteTime >= AllowTime_EasyCard[4] + AllowTime_EasyCard[5])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "悠遊卡寫卡超時" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        TestTimesCount(1);
                                    }
                                    //紀錄測試參數
                                }




                                ReaderStatus = 0;
                            }
                            else
                            {
                                TestTimesCount(0);
                                richTextBox1.Text += "交易失敗" + Environment.NewLine;
                                Array.Clear(CardID, 0, CardID.Length);
                                ReaderStatus = 0;
                            }
                            REC = false;
                        }
                        break;
                    }
                case 51:    //一卡通讀卡
                    {
                        if (REC == true)
                        {
                            richTextBox2.Text += "一卡通讀卡[" + Convert.ToString(RecievedQ.Count) + "]:" + BitConverter.ToString(RecievedQ.ToArray()) + Environment.NewLine;

                            if (ReaderFramewareVersion == 1)
                            {
                                if (RecievedQ.Count == 128 || RecievedQ.Count == 127)
                                {
                                    Watch.Stop();
                                    ReadTime = Watch.ElapsedMilliseconds;
                                    ipass.Len = RecievedQ.Count;
                                    ipass.BytesToReadStruct(RecievedQ.ToArray());
                                    RecievedQ.Clear();
                                    if (SearchCard_BT == true || mode == false)
                                    {
                                        richTextBox1.Text += "餘額:" + ipass.Get_evValue() + Environment.NewLine;
                                        if (ipass.Get_evValue() >= 0)//執行寫卡動作
                                        {
                                            ReaderStatus = 52;
                                            EnQTRAN(52);
                                        }
                                        else
                                        {
                                            richTextBox1.Text += "餘額不足" + Environment.NewLine;
                                            ReaderStatus = 0;
                                        }
                                    }
                                    else
                                    {
                                        richTextBox1.Text += "查卡資訊" + Environment.NewLine;
                                        ipass.PrintInfo();
                                        ReaderStatus = 0;
                                    }
                                }
                                else
                                {
                                    TestTimesCount(0);
                                    richTextBox1.Text += "讀卡失敗" + Environment.NewLine;
                                    Array.Clear(CardID, 0, CardID.Length);
                                    if (!mode)
                                    {
                                        ReaderStatus = 5;
                                        break;
                                    }
                                    else { ReaderStatus = 0; }
                                    
                                }
                            }
                            else if (ReaderFramewareVersion == 2|| ReaderFramewareVersion == 3)
                            {
                                if (RecievedQ.Count == 204)
                                {
                                    Watch.Stop();
                                    ReadTime = Watch.ElapsedMilliseconds;
                                    ipass.Len = RecievedQ.Count;
                                    ipass.BytesToReadStruct(RecievedQ.ToArray());
                                    RecievedQ.Clear();
                                    if (SearchCard_BT == true || mode == false)
                                    {
                                        richTextBox1.Text += "餘額:" + ipass.Get_evValue() + Environment.NewLine;
                                        if (ipass.Get_evValue() >= 0)
                                        {
                                            ReaderStatus = 52;
                                            EnQTRAN(52);
                                        }
                                        else
                                        {
                                            richTextBox1.Text += "餘額不足" + Environment.NewLine;
                                            ReaderStatus = 0;
                                        }
                                    }
                                    else
                                    {
                                        richTextBox1.Text += "查卡資訊" + Environment.NewLine;
                                        ipass.PrintInfo();
                                        ReaderStatus = 0;
                                    }
                                }
                                else
                                {
                                    TestTimesCount(0);
                                    richTextBox1.Text += "讀卡失敗" + Environment.NewLine;
                                    Array.Clear(CardID, 0, CardID.Length);
                                    if (!mode)
                                    {
                                        ReaderStatus = 5;
                                        break;
                                    }
                                    else { ReaderStatus = 0; }
                                }
                            }
                            else
                            {
                                richTextBox1.Text += "卡機類型錯誤" + Environment.NewLine;
                            }

                                REC = false;
                        }
                        break;
                    }
                case 52:    //一卡通寫卡
                    {
                        if (REC == true)
                        {
                            byte[] Recieved = RecievedQ.ToArray();
                            RecievedQ.Clear();
                            richTextBox2.Text += "一卡通寫卡[" + Convert.ToString(Recieved.Length) + "]:" + BitConverter.ToString(Recieved) + Environment.NewLine;
                            if (Recieved.Length == 28)
                            {
                                Watch.Stop();
                                WriteTime = Watch.ElapsedMilliseconds;
                                if (Recieved[2] == 0x02)
                                {
                                    richTextBox1.Text += "交易成功(上)" + Environment.NewLine;
                                }
                                else
                                {
                                    richTextBox1.Text += "交易成功(下)" + Environment.NewLine;
                                }

                                SingleTestTimeLableChange();
                                HistoryVelocity();

                                if (mode == false)
                                {
                                    if (FindTime >= AllowTime_IPass[0] + AllowTime_IPass[1])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "一卡通尋卡超時" + Environment.NewLine;
                                    }
                                    else if (ReadTime >= AllowTime_IPass[2] + AllowTime_IPass[3])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "一卡通讀卡超時" + Environment.NewLine;
                                    }
                                    else if (WriteTime >= AllowTime_IPass[4] + AllowTime_IPass[5])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "一卡通寫卡超時" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        TestTimesCount(1);
                                    }


                                    if (!mode) 
                                    {
                                        CreatSensingTestData(Convert.ToInt32(DVID_TB.Text), "IPass", SensingTestCount, ThickChange);
                                        
                                        if (TestBtnCount % 5 == 0 && SensingTestCount == SensingTestMiddleCount)
                                        {
                                            ThickChange = 5;
                                            start = false;
                                            richTextBox1.Text += $"替換為{ThickChange}公分治具\n繼續以一卡通卡片測試\n";
                                        }
                                        else if(SensingTestCount == SensingTestFinalCount)//FRW結果 
                                        {
                                            start = false;
                                            richTextBox1.Text += "替換為1公分治具與ICash卡片\n置於天線版中間進行測試";
                                            SensingTestCount = 0;
                                            ThickChange = 1;
                                        }

                                    }

                                }
                                ReaderStatus = 0;                                
                                TestBtnEnable(SensingTestCount, $"{(NowCardTestType)1}", ThickChange);
                            }
                            else
                            {
                                TestTimesCount(0);
                                richTextBox1.Text += "交易失敗" + Environment.NewLine;
                                Array.Clear(CardID, 0, CardID.Length);
                                ReaderStatus = 0;
                            }

                            REC = false;
                        }
                        break;
                    }
                case 61:    //愛金卡讀卡
                    {
                        if (REC == true)
                        {
                            richTextBox2.Text += "愛金卡讀卡:" + BitConverter.ToString(RecievedQ.ToArray()) + Environment.NewLine;
                            if (RecievedQ.Count == 133)
                            {
                                Watch.Stop();
                                ReadTime = Watch.ElapsedMilliseconds;
                                icash.BytesToReadStruct(RecievedQ.ToArray());
                                RecievedQ.Clear();
                                if (SearchCard_BT == true || mode == false)
                                {
                                    richTextBox1.Text += "餘額:" + icash.Get_Card_Balance() + Environment.NewLine;
                                    if (icash.Get_Card_Balance() >= 0)
                                    {
                                        ReaderStatus = 62;
                                        EnQTRAN(62);
                                    }
                                    else
                                    {
                                        richTextBox1.Text += "餘額不足" + Environment.NewLine;
                                        ReaderStatus = 0;
                                    }
                                }
                                else
                                {
                                    icash.PrintInfo();
                                    ReaderStatus = 0;
                                }
                            }
                            else
                            {
                                TestTimesCount(0);
                                richTextBox1.Text += "讀卡失敗" + Environment.NewLine;
                                Array.Clear(CardID, 0, CardID.Length);
                                if (!mode)
                                {
                                    ReaderStatus = 5;
                                    break;
                                }
                                else { ReaderStatus = 0; }
                            }
                            REC = false;
                        }
                        break;
                    }
                case 62:    //愛金卡寫卡
                    {
                        if (REC == true)
                        {
                            byte[] Recieved = RecievedQ.ToArray();
                            RecievedQ.Clear();
                            richTextBox2.Text += "愛金卡寫卡:" + BitConverter.ToString(Recieved) + Environment.NewLine;
                            if (Recieved.Length == 264)
                            {
                                Watch.Stop();
                                WriteTime = Watch.ElapsedMilliseconds;
                                if (icash.Get_Trans_Type() == 0x00)
                                {
                                    richTextBox1.Text += "交易成功(上)" + Environment.NewLine;
                                }
                                else
                                {
                                    richTextBox1.Text += "交易成功(下)" + Environment.NewLine;
                                }

                                SingleTestTimeLableChange();
                                HistoryVelocity();

                                if (mode == false)
                                {
                                    if (FindTime >= AllowTime_IPass[0] + AllowTime_IPass[1])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "ICash尋卡超時" + Environment.NewLine;
                                    }
                                    else if (ReadTime >= AllowTime_IPass[2] + AllowTime_IPass[3])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "ICash讀卡超時" + Environment.NewLine;
                                    }
                                    else if (WriteTime >= AllowTime_IPass[4] + AllowTime_IPass[5])
                                    {
                                        TestTimesCount(0);
                                        richTextBox1.Text += "ICash寫卡超時" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        TestTimesCount(1);
                                    }


                                    if (!mode)
                                    {
                                        CreatSensingTestData(Convert.ToInt32(DVID_TB.Text), "ICash", SensingTestCount, ThickChange);
                                        
                                        if (TestBtnCount % 5 == 0 && SensingTestCount == SensingTestMiddleCount)
                                        {                                            
                                            start = false;
                                            ThickChange = 5;
                                            richTextBox1.Text += $"替換為{ThickChange}公分治具\n並繼續以ICash卡片測試";
                                        }

                                        if (SensingTestCount == SensingTestFinalCount)//FRW結果 
                                        {
                                            TestResult[2] = "O";
                                            TestResult[3] = $"{(AFindTime + AReadTime + AWriteTime) / SensingTestCount}";
                                            TestResult[4] = $"{HFTTime}";
                                            TestResult[5] = $"{HSTTime}";
                                            ReaderStatus = 5;
                                            break;
                                        }

                                    }

                                }
                                ReaderStatus = 0;                               
                                TestBtnEnable(SensingTestCount, $"{(NowCardTestType)2}", ThickChange);
                            }
                            else
                            {
                                TestTimesCount(0);
                                richTextBox1.Text += "交易失敗" + Environment.NewLine;
                                Array.Clear(CardID, 0, CardID.Length);
                                ReaderStatus = 0;
                            }
                            REC = false;
                        }
                        break;
                    }
            }
        }
        private void UI_ini()
        {
            start = false;
            Start_BT.Enabled = false;
            Start_BT_Forward.Enabled = false;
            Start_BT_Back.Enabled = false;
            Start_BT_Left.Enabled = false;
            Start_BT_Right.Enabled = false;
            SAMSlotTest_Btn.Enabled = false;
            SAMExSlotTest_Btn.Enabled = false;
            DVID_TB.Enabled = true;

            Times = StandardTimes;

            //重置尋讀寫測試參數

            TestFail = 0;
            TestSuccess = 0;
            //單次測試
            OFTime_Lab.Text = "0";
            ORTime_Lab.Text = "0";
            OWTime_Lab.Text = "0";
            OTTime_Lab.Text = "0";
            //歷史最快最慢
            HFTTime = 0;
            HSTTime = 0;

            HFFTime_Lab.Text = "0";
            HFRTime_Lab.Text = "0";
            HFWTime_Lab.Text = "0";
            HFTTime_Lab.Text = "0";

            HSFTime_Lab.Text = "0";
            HSRTime_Lab.Text = "0";
            HSWTime_Lab.Text = "0";
            HSTTime_Lab.Text = "0";
            //測試平均值
            AFindTime = 0;
            AReadTime = 0;
            AWriteTime = 0;
            AFTime_Lab.Text = "0";
            ARTime_Lab.Text = "0";
            AWTime_Lab.Text = "0";
            ATTime_Lab.Text = "0";

            //測試成功失敗次數重置
            TestSuccessLab.Text = "0";
            TestFailLab.Text = "0";

            ThickChange = 1;

            
            richTextBox1.Clear();

            SensingTestCount = 0;
            for (int i = 0; i < 5; i++) { SAMSlotStatus[i] = 0; }
            for (int i = 0; i < 3; i++) { TestResult[i] = "None"; }
            ReaderStatus = 4;


        }
        private void TestBtnEnable(int SensingStatus,string CardType,int Thick) 
        {
            int TestTimes = 5;

            if (SensingStatus % TestTimes == 0) 
            {
                start = false;
            }
                

            if (SensingStatus == TestTimes)
            {                
                richTextBox1.Text = $"將'{Thick}cm'治具與{CardType}卡片中間\n置於天線上方\n\n點選上方位置天線測試" + Environment.NewLine; ;                
                Start_BT_Forward.Enabled = true;

            }
            else if (SensingStatus == TestTimes*2)
            {

                richTextBox1.Text = $"將'{Thick}cm'治具與{CardType}卡片中間\n置於天線下方\n\n點選下方位置天線測試" + Environment.NewLine;                
                Start_BT_Back.Enabled = true;

            }
            else if (SensingStatus == TestTimes*3)
            {
                richTextBox1.Text = $"將'{Thick}cm'治具與{CardType}卡片置於天線左方\n\n點選左方位置天線測試" + Environment.NewLine; ;
               
                Start_BT_Left.Enabled = true;                
            }
            else if (SensingStatus == TestTimes*4)
            {
                richTextBox1.Text = $"將'{Thick}cm'治具與{CardType}卡片置於天線右方\n\n點選右方位置天線測試" + Environment.NewLine; ;                
                Start_BT_Right.Enabled = true;
            }
            else if (SensingStatus == TestTimes*5)
            {
                Start_BT.Enabled = true;
            }
            else if (SensingStatus == TestTimes*6)
            {
                ThickChange = 3;
                Thick = ThickChange;
                richTextBox1.Text = $"將'{Thick}cm'治具與{CardType}卡片中間置於天線上方\n\n點選上方位置天線測試" + Environment.NewLine; 
               
                Start_BT_Forward.Enabled = true;
            }
            else if (SensingStatus == TestTimes*7)
            {
                richTextBox1.Text = $"將'{Thick}cm'治具與{CardType}卡片中間置於天線下方\n\n點選下方位置天線測試" + Environment.NewLine; 
 
                Start_BT_Back.Enabled = true;
            }
            else if (SensingStatus == TestTimes*8)
            {
                richTextBox1.Text = $"將'{Thick}cm'治具與{CardType}卡片置於天線左方\n\n點選左方位置天線測試" + Environment.NewLine;
                
                Start_BT_Left.Enabled = true;
            }
            else if (SensingStatus == TestTimes*9)
            {
                richTextBox1.Text = $"將'{Thick}cm'治具與{CardType}卡片置於天線右方\n\n點選右方位置天線測試" + Environment.NewLine;           
                Start_BT_Right.Enabled = true;
            }
            else if (SensingStatus == 0)
            {
                //richTextBox1.Text += $"將'{Thick}cm'治具與{CardType}卡片置於天線中間位置\n\n點選中間位置天線測試" + Environment.NewLine;
                Start_BT.Enabled = true;
            }
            else 
            {
                richTextBox1.Text = "測試鈕數值未定義";
            }

        }

        private byte LRCFuntion(byte[] LRCData) 
        {
            int i=0;
            byte LRCResult = 0;
            bool Leni=LRCData.Length>LRCData.Length - 3;
            foreach (byte x in LRCData) 
            {
                if (LRCData.Length - 3 == i)
                {
                    break;
                }
                if (i >= 5)
                {
                    LRCResult^= x;
                }
                i++;
            }

            return LRCResult;
        }
        private void CreatSensingTestData(int DeviceID,string RFIDType,int Testedcount,int ThickChanged) 
        {

            String TestDataFileName = "D"+$"{DeviceID}" + "_TestData.csv", now;            
           
            now = $"{DateTime.Now}";

            //紀錄測試參數
            if (!File.Exists(TestDataFileName))
            {
                using (StreamWriter sw = new StreamWriter(TestDataFileName, true,System.Text.Encoding.Default))
                {

                    sw.WriteLine(
                        "測試次數"+","+"ReaderApVersion" + "," + "RFIDType,"+ "尋卡時間" + "," + "讀卡時間" + ","
                        + "寫卡時間" + "," + "總時間," +"測試時間"
                        );
                    sw.WriteLine(
                          SensingPosition+"_"+$"{ThickChanged}cm_" + "第" +$"{Testedcount}"+"次測試"+","+ ReaderApVersion + "," + $"{RFIDType}" + ","+ FindTime.ToString() + "," + ReadTime.ToString() + ","
                        + WriteTime.ToString() + "," + (FindTime + ReadTime + WriteTime).ToString()+","+ now
                        );
                }
            }

            else
            {
                using (StreamWriter sw = new StreamWriter(TestDataFileName, true, System.Text.Encoding.Default))
                {

                    sw.WriteLine(
                          SensingPosition + "_" + $"{ThickChanged}cm_" + "第" + $"{Testedcount}" + "次測試" + "," + ReaderApVersion + "," + $"{RFIDType}" + "," + FindTime.ToString() + "," + ReadTime.ToString() + ","
                        + WriteTime.ToString() + "," + (FindTime + ReadTime + WriteTime).ToString() + "," + now
                        );
                }
            }

        }

        private void CreatSAMTestData(int DeviceID, int[] SAMSlotStatus)
        {

            String TestDataFileName = "SAMSlot_TestData.csv";

            //紀錄測試參數
            if (!File.Exists(TestDataFileName))
            {
                using (StreamWriter sw = new StreamWriter(TestDataFileName, true, System.Text.Encoding.Default))
                {

                    sw.WriteLine(
                        "DeviceID" + ","+"台智卡,"+"YHDP,"+"悠遊卡,"+"IPass,"+"ICash"
                        );
                    sw.WriteLine(
                          "D"+$"{DeviceID}," + $"SAM{SAMSlotStatus[0]}," + $"SAM{SAMSlotStatus[1]}," + $"SAM{SAMSlotStatus[2]}," + $"SAM{SAMSlotStatus[3]}," + $"SAM{SAMSlotStatus[4]},"
                        );              

                }
            }

            else
            {
                using (StreamWriter sw = new StreamWriter(TestDataFileName, true, System.Text.Encoding.Default))
                {

                    sw.WriteLine("D" + $"{DeviceID}," + $"SAM{SAMSlotStatus[0]}," + $"SAM{SAMSlotStatus[1]}," + $"SAM{SAMSlotStatus[2]}," + $"SAM{SAMSlotStatus[3]}," + $"SAM{SAMSlotStatus[4]},");
  
                }
            }

        }

        private void CreatTestResultData(int DeviceID, string[] TestResultArray)
        {

            String TestDataFileName = "TestResultData.csv";

            //紀錄測試參數
            if (!File.Exists(TestDataFileName))
            {
                using (StreamWriter sw = new StreamWriter(TestDataFileName, true, System.Text.Encoding.Default))
                {

                    sw.WriteLine(
                        "DeviceID" + "," + "SAMSlot_Bottom," + "SAMSlot_Bottom,"+"FRWResult,"+"平均,"+"最快,"+"最慢,"
                        );
                    sw.WriteLine(
                          "D" + $"{DeviceID}," + $"{TestResultArray[0]}," + $"{TestResultArray[1]}," + $"{TestResultArray[2]}," + $"{TestResultArray[3]}," + $"{TestResultArray[4]}," + $"{TestResultArray[5]},"
                        );

                }
            }

            else
            {
                using (StreamWriter sw = new StreamWriter(TestDataFileName, true, System.Text.Encoding.Default))
                {

                    sw.WriteLine(
                          "D" + $"{DeviceID}," + $"{TestResultArray[0]}," + $"{TestResultArray[1]}," + $"{TestResultArray[2]}," + $"{TestResultArray[3]}," + $"{TestResultArray[4]}," + $"{TestResultArray[5]},"
                        );

                }
            }

        }


    }
    public class ipass
    {
        private KRTC_Read ipassRead;
        private KRTC_Write ipassWrite;
        public int Len = 0;
        public Form2 F2;
        //=====================================================================
        public void initialization()
        {
            ipassRead.csn = new byte[16];
            ipassRead.evValue = new byte[4];
            ipassRead.bevValue = new byte[4];
            ipassRead.syncValue = new byte[4];
            ipassRead.persionalID = new byte[6];
            ipassRead.speActivationTime = new byte[4];
            ipassRead.speExipreTime = new byte[4];
            ipassRead.speResetTime = new byte[4];
            ipassRead.speRouteID = new byte[2];
            ipassRead.speUseNo = new byte[2];
            ipassRead.preBUSRoute = new byte[2];
            ipassRead.preBUSTime = new byte[4];
            ipassRead.preBUSNo = new byte[2];
            ipassRead.speHUsedNo = new byte[2];
            ipassRead.cardTxnsNumber = new byte[2];
            ipassRead.preGenTime = new byte[4];
            ipassRead.preTxnsValue = new byte[2];
            ipassRead.prePostValue = new byte[2];
            ipassRead.preTxnsMachine = new byte[4];
            ipassRead.trfID = new byte[2];
            ipassRead.traExpireTime = new byte[4];
            ipassRead.personalTime = new byte[4];
            ipassRead.trfGenTime = new byte[4];
            ipassRead.trfRouteID = new byte[2];
            ipassRead.trfTxnsMachine = new byte[2];
            ipassRead.MaaSTransport = new byte[4];
            ipassRead.MaaSStartDate = new byte[4];
            ipassRead.MaaSEndDate = new byte[4];

            ipassRead.MaaSStartEndStation1.StartStation = new byte[2];
            ipassRead.MaaSStartEndStation1.EndStation = new byte[2];
            ipassRead.MaaSStartEndStation1.RouteID = new byte[2];
            ipassRead.MaaSStartEndStation1.Used.UsedValue = new byte[2];

            ipassRead.MaaSStartEndStation2.StartStation = new byte[2];
            ipassRead.MaaSStartEndStation2.EndStation = new byte[2];
            ipassRead.MaaSStartEndStation2.RouteID = new byte[2];
            ipassRead.MaaSStartEndStation2.Used.UsedValue = new byte[2];

            ipassRead.MaaSStartEndStation3.StartStation = new byte[2];
            ipassRead.MaaSStartEndStation3.EndStation = new byte[2];
            ipassRead.MaaSStartEndStation3.RouteID = new byte[2];
            ipassRead.MaaSStartEndStation3.Used.UsedValue = new byte[2];

            ipassRead.MaaSStartEndStation4.StartStation = new byte[2];
            ipassRead.MaaSStartEndStation4.EndStation = new byte[2];
            ipassRead.MaaSStartEndStation4.RouteID = new byte[2];
            ipassRead.MaaSStartEndStation4.Used.UsedValue = new byte[2];

            ipassRead.CardNumber = new byte[16];
            ipassRead.EnterpriseCode = new byte[2];
            ipassRead.CardTransactionSerialNumber = new byte[2];
            ipassRead.CardExpireTime = new byte[2];


            //=====================================================================
            ipassWrite.csn = new byte[16];
            ipassWrite.txnsValue = new byte[2];
            ipassWrite.txnsTime = new byte[4];
            ipassWrite.txnsMachine = new byte[2];
            ipassWrite.routeID = new byte[2];
            ipassWrite.speHUsedNo = new byte[2];
            ipassWrite.speResetTime = new byte[4];
            ipassWrite.trfID = new byte[2];
            ipassWrite.traExpireTime = new byte[4];
            ipassWrite.welExpireTime = new byte[4];
            ipassWrite.speUseNo = new byte[2];

            ipassWrite.MaaSStartEndStation1.StartStation = new byte[2];
            ipassWrite.MaaSStartEndStation1.EndStation = new byte[2];
            ipassWrite.MaaSStartEndStation1.RouteID = new byte[2];
            ipassWrite.MaaSStartEndStation1.Used.UsedValue = new byte[2];

            ipassWrite.MaaSStartEndStation2.StartStation = new byte[2];
            ipassWrite.MaaSStartEndStation2.EndStation = new byte[2];
            ipassWrite.MaaSStartEndStation2.RouteID = new byte[2];
            ipassWrite.MaaSStartEndStation2.Used.UsedValue = new byte[2];

            ipassWrite.MaaSStartEndStation3.StartStation = new byte[2];
            ipassWrite.MaaSStartEndStation3.EndStation = new byte[2];
            ipassWrite.MaaSStartEndStation3.RouteID = new byte[2];
            ipassWrite.MaaSStartEndStation3.Used.UsedValue = new byte[2];

            ipassWrite.MaaSStartEndStation4.StartStation = new byte[2];
            ipassWrite.MaaSStartEndStation4.EndStation = new byte[2];
            ipassWrite.MaaSStartEndStation4.RouteID = new byte[2];
            ipassWrite.MaaSStartEndStation4.Used.UsedValue = new byte[2];
        }
        public void BytesToReadStruct(byte[] Recieved)//比文件中總byte數多1
        {
            Array.Copy(Recieved, 5, ipassRead.csn, 0, 16);
            Array.Copy(Recieved, 21, ipassRead.evValue, 0, 4);
            Array.Copy(Recieved, 25, ipassRead.bevValue, 0, 4);
            Array.Copy(Recieved, 29, ipassRead.syncValue, 0, 4);
            Array.Copy(Recieved, 33, ipassRead.persionalID, 0, 6);
            ipassRead.cardType = Recieved[39];
            ipassRead.speProviderType = Recieved[40];
            ipassRead.speProviderID = Recieved[41];
            Array.Copy(Recieved, 42, ipassRead.speActivationTime, 0, 4);
            Array.Copy(Recieved, 46, ipassRead.speExipreTime, 0, 4);
            Array.Copy(Recieved, 50, ipassRead.speResetTime, 0, 4);
            ipassRead.spestStation = Recieved[54];
            ipassRead.speedStation = Recieved[55];
            Array.Copy(Recieved, 56, ipassRead.speRouteID, 0, 2);
            Array.Copy(Recieved, 58, ipassRead.speUseNo, 0, 2);
            ipassRead.busConveyancer = Recieved[60];
            ipassRead.preBUSAreaCode = Recieved[61];
            ipassRead.preBUSCompID = Recieved[62];
            Array.Copy(Recieved, 63, ipassRead.preBUSRoute, 0, 2);
            ipassRead.preBUSStation = Recieved[65];
            Array.Copy(Recieved, 66, ipassRead.preBUSTime, 0, 4);
            ipassRead.preBUSStatus = Recieved[70];
            ipassRead.preTravelType = Recieved[71];
            Array.Copy(Recieved, 72, ipassRead.preBUSNo, 0, 2);
            Array.Copy(Recieved, 74, ipassRead.speHUsedNo, 0, 2);
            Array.Copy(Recieved, 76, ipassRead.cardTxnsNumber, 0, 2);
            Array.Copy(Recieved, 78, ipassRead.preGenTime, 0, 4);
            ipassRead.preGenClass = Recieved[82];
            Array.Copy(Recieved, 83, ipassRead.preTxnsValue, 0, 2);
            Array.Copy(Recieved, 85, ipassRead.prePostValue, 0, 2);
            ipassRead.preGenSystem = Recieved[87];
            ipassRead.preTxnsLocaion = Recieved[88];
            Array.Copy(Recieved, 89, ipassRead.preTxnsMachine, 0, 4);
            ipassRead.preSale = Recieved[93];
            ipassRead.syncStatus = Recieved[94];
            Array.Copy(Recieved, 95, ipassRead.trfID, 0, 2);
            ipassRead.transferFlag = Recieved[97];
            ipassRead.travelDay = Recieved[98];
            Array.Copy(Recieved, 99, ipassRead.traExpireTime, 0, 4);
            Array.Copy(Recieved, 103, ipassRead.personalTime, 0, 4);
            Array.Copy(Recieved, 107, ipassRead.trfGenTime, 0, 4);
            ipassRead.trfspeCurrentSystemID = Recieved[111];
            ipassRead.trfprePreviousSystemID = Recieved[112];
            ipassRead.trfTxnsType = Recieved[113];
            ipassRead.trfCompID = Recieved[114];
            ipassRead.trfTxnsLocation = Recieved[115];
            Array.Copy(Recieved, 116, ipassRead.trfRouteID, 0, 2);
            Array.Copy(Recieved, 118, ipassRead.trfTxnsMachine, 0, 2);
            ipassRead.regFlag = Recieved[120];
            ipassRead.cardStatus = Recieved[121];
            ipassRead.identifyRegStudentCard = Recieved[122];
            ipassRead.monthlyCompID = Recieved[123];
            if (Len > 127)
                ipassRead.evenRideBit = Recieved[124];
            if (Len > 128)
            {
                ipassRead.MaaSCard = Recieved[125];
                ipassRead.MappingType = Recieved[126];
                ipassRead.MaaSAreaCode = Recieved[127];
                Array.Copy(Recieved, 128, ipassRead.MaaSTransport, 0, 4);
                ipassRead.MaaSPeriodCode = Recieved[132];
                ipassRead.MaaSPeriod = Recieved[133];
                Array.Copy(Recieved, 134, ipassRead.MaaSStartDate, 0, 4);
                Array.Copy(Recieved, 138, ipassRead.MaaSEndDate, 0, 4);

                ipassRead.MaaSStartEndStation1.TransportSetting = Recieved[142];
                Array.Copy(Recieved, 143, ipassRead.MaaSStartEndStation1.StartStation, 0, 2);
                Array.Copy(Recieved, 145, ipassRead.MaaSStartEndStation1.EndStation, 0, 2);
                Array.Copy(Recieved, 147, ipassRead.MaaSStartEndStation1.RouteID, 0, 2);
                ipassRead.MaaSStartEndStation1.Used.MaxUsage = Recieved[149];
                ipassRead.MaaSStartEndStation1.Used.RemainingUse = Recieved[150];
                Array.Copy(Recieved, 151, ipassRead.MaaSStartEndStation1.Used.UsedValue, 0, 2);

                ipassRead.MaaSStartEndStation2.TransportSetting = Recieved[153];
                Array.Copy(Recieved, 154, ipassRead.MaaSStartEndStation2.StartStation, 0, 2);
                Array.Copy(Recieved, 156, ipassRead.MaaSStartEndStation2.EndStation, 0, 2);
                Array.Copy(Recieved, 158, ipassRead.MaaSStartEndStation2.RouteID, 0, 2);
                ipassRead.MaaSStartEndStation2.Used.MaxUsage = Recieved[160];
                ipassRead.MaaSStartEndStation2.Used.RemainingUse = Recieved[161];
                Array.Copy(Recieved, 162, ipassRead.MaaSStartEndStation2.Used.UsedValue, 0, 2);

                ipassRead.MaaSStartEndStation3.TransportSetting = Recieved[164];
                Array.Copy(Recieved, 165, ipassRead.MaaSStartEndStation3.StartStation, 0, 2);
                Array.Copy(Recieved, 167, ipassRead.MaaSStartEndStation3.EndStation, 0, 2);
                Array.Copy(Recieved, 169, ipassRead.MaaSStartEndStation3.RouteID, 0, 2);
                ipassRead.MaaSStartEndStation3.Used.MaxUsage = Recieved[171];
                ipassRead.MaaSStartEndStation3.Used.RemainingUse = Recieved[172];
                Array.Copy(Recieved, 173, ipassRead.MaaSStartEndStation3.Used.UsedValue, 0, 2);

                ipassRead.MaaSStartEndStation4.TransportSetting = Recieved[175];
                Array.Copy(Recieved, 176, ipassRead.MaaSStartEndStation4.StartStation, 0, 2);
                Array.Copy(Recieved, 178, ipassRead.MaaSStartEndStation4.EndStation, 0, 2);
                Array.Copy(Recieved, 180, ipassRead.MaaSStartEndStation4.RouteID, 0, 2);
                ipassRead.MaaSStartEndStation4.Used.MaxUsage = Recieved[182];
                ipassRead.MaaSStartEndStation4.Used.RemainingUse = Recieved[183];
                Array.Copy(Recieved, 184, ipassRead.MaaSStartEndStation4.Used.UsedValue, 0, 2);

                Array.Copy(Recieved, 186, ipassRead.CardNumber, 0, 15);//應該為16


            }
        }
        public byte[] GetWriteStruct()//須修改
        {
            var MemStream = new MemoryStream();
            var Binwriter = new BinaryWriter(MemStream);
            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);

            ipassWrite.txnsTime = BitConverter.GetBytes(unixTime);
            ipassWrite.csn = ipassRead.csn;//16
            ipassWrite.txnsSystem = ipassRead.preGenSystem;
            ipassWrite.txnsLocation = ipassRead.preTxnsLocaion;
            Array.Copy(ipassRead.preTxnsMachine, 2, ipassWrite.txnsMachine, 0, 2);
            ipassWrite.compID = ipassRead.preBUSCompID;
            ipassWrite.routeID = ipassRead.preBUSRoute;
            ipassWrite.travelType = 0x01;
            ipassWrite.speHUsedNo = ipassRead.speHUsedNo;
            ipassWrite.speResetTime = ipassRead.speResetTime;
            ipassWrite.areaCode = ipassRead.preBUSAreaCode;
            ipassWrite.trfID = ipassRead.trfID;
            ipassWrite.travelType = 0x01;
            if (ipassRead.preBUSStatus == 0x00)
            {
                ipassWrite.txnsType = 0x00;
            }
            else
            {
                ipassWrite.txnsType = 0x10;
            }
            Binwriter.Write(ipassWrite.csn);
            Binwriter.Write(ipassWrite.txnsValue);
            Binwriter.Write(ipassWrite.txnsTime);
            Binwriter.Write(ipassWrite.txnsType);
            Binwriter.Write(ipassWrite.txnsSystem);
            Binwriter.Write(ipassWrite.txnsLocation);
            Binwriter.Write(ipassWrite.txnsMachine);
            Binwriter.Write(ipassWrite.compID);
            Binwriter.Write(ipassWrite.routeID);
            Binwriter.Write(ipassWrite.travelType);
            Binwriter.Write(ipassWrite.speHUsedNo);
            Binwriter.Write(ipassWrite.speResetTime);
            Binwriter.Write(ipassWrite.permitFlag);
            Binwriter.Write(ipassWrite.areaCode);
            Binwriter.Write(ipassWrite.trfID);
            Binwriter.Write(ipassWrite.txnspreSale);
            Binwriter.Write(ipassWrite.traExpireTime);
            Binwriter.Write(ipassWrite.welExpireTime);
            Binwriter.Write(ipassWrite.speUseNo);
            Binwriter.Write(ipassWrite.incrEvenRideBit);

            if (Len > 128) //須修改
            {
                /*
                ipassWrite.MaaSStartEndStation1 = ipassRead.MaaSStartEndStation1;
                ipassWrite.MaaSStartEndStation2 = ipassRead.MaaSStartEndStation2;
                ipassWrite.MaaSStartEndStation3 = ipassRead.MaaSStartEndStation3;
                ipassWrite.MaaSStartEndStation4 = ipassRead.MaaSStartEndStation4;
                //MaaSStartEndStation1
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.TransportSetting);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.StartStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.EndStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.RouteID);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.Used.MaxUsage);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.Used.RemainingUse);
                Binwriter.Write(ipassWrite.MaaSStartEndStation1.Used.UsedValue);
                //MaaSStartEndStation2
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.TransportSetting);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.StartStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.EndStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.RouteID);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.Used.MaxUsage);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.Used.RemainingUse);
                Binwriter.Write(ipassWrite.MaaSStartEndStation2.Used.UsedValue);
                //MaaSStartEndStation3
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.TransportSetting);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.StartStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.EndStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.RouteID);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.Used.MaxUsage);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.Used.RemainingUse);
                Binwriter.Write(ipassWrite.MaaSStartEndStation3.Used.UsedValue);
                //MaaSStartEndStation4
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.TransportSetting);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.StartStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.EndStation);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.RouteID);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.Used.MaxUsage);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.Used.RemainingUse);
                Binwriter.Write(ipassWrite.MaaSStartEndStation4.Used.UsedValue);
                */
            }


            return MemStream.ToArray();
        }
        public byte Get_preBUSStatus()
        {
            return ipassRead.preBUSStatus;
        }
        public int Get_evValue()
        {
            return BitConverter.ToInt32(ipassRead.evValue, 0);
        }
        public void PrintInfo()//maas還沒加入
        {
            F2.richTextBox1.Text = "卡別 : IPass(一卡通)" + Environment.NewLine;
            F2.richTextBox1.Text += "卡號 : " + BitConverter.ToString(ipassRead.csn, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "主要電子票值 : " + BitConverter.ToInt32(ipassRead.evValue, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "備份電子票值 : " + BitConverter.ToInt32(ipassRead.bevValue, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "同步後電子票值 : " + BitConverter.ToInt32(ipassRead.syncValue, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "身份證字號 : " + BitConverter.ToString(ipassRead.persionalID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "公車端票卡種類 : " + Convert.ToString(ipassRead.cardType) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票識別身分 : " + Convert.ToString(ipassRead.speProviderType) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票識別單位 : " + Convert.ToString(ipassRead.speProviderID) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票識別起始日 : " + BitConverter.ToInt16(ipassRead.speActivationTime, 0) + "年" + BitConverter.ToString(ipassRead.speActivationTime, 2, 1) + "月" + BitConverter.ToString(ipassRead.speActivationTime, 3, 1) + "日" + Environment.NewLine;
            F2.richTextBox1.Text += "特種票識別有效日 : " + BitConverter.ToInt16(ipassRead.speExipreTime, 0) + "年" + BitConverter.ToString(ipassRead.speExipreTime, 2, 1) + "月" + BitConverter.ToString(ipassRead.speExipreTime, 3, 1) + "日" + Environment.NewLine;
            F2.richTextBox1.Text += "特種票重置日期 : " + BitConverter.ToInt16(ipassRead.speResetTime, 0) + "年" + BitConverter.ToString(ipassRead.speResetTime, 2, 1) + "月" + BitConverter.ToString(ipassRead.speResetTime, 3, 1) + "日" + Environment.NewLine;
            F2.richTextBox1.Text += "特種票起站代碼 : " + Convert.ToString(ipassRead.spestStation) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票迄站代碼 : " + Convert.ToString(ipassRead.speedStation) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票路線編號 : " + BitConverter.ToInt16(ipassRead.speRouteID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票使用限次 : " + BitConverter.ToInt16(ipassRead.speUseNo, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "公車業者代碼 : " + Convert.ToString(ipassRead.busConveyancer) + Environment.NewLine;
            F2.richTextBox1.Text += "上次公車端區碼 : " + Convert.ToString(ipassRead.preBUSAreaCode) + Environment.NewLine;
            F2.richTextBox1.Text += "上次公車端交易運輸業者 : " + Convert.ToString(ipassRead.preBUSCompID) + Environment.NewLine;
            F2.richTextBox1.Text += "上次公車端交易路線 : " + BitConverter.ToInt16(ipassRead.preBUSRoute, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "上次公車端交易站號 : " + Convert.ToString(ipassRead.preBUSStation) + Environment.NewLine;
            F2.richTextBox1.Text += "上次公車端交易時間 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(ipassRead.preBUSTime, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "上次公車端搭乘狀態 : " + Convert.ToString(ipassRead.preBUSStatus) + Environment.NewLine;
            F2.richTextBox1.Text += "上次搭乘類型 : " + Convert.ToString(ipassRead.preTravelType) + Environment.NewLine;
            F2.richTextBox1.Text += "上次公車端交易驗票機編號 : " + BitConverter.ToInt16(ipassRead.preBUSNo, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票已使用次數 : " + BitConverter.ToInt16(ipassRead.speHUsedNo, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "卡片交易序號 : " + BitConverter.ToInt16(ipassRead.cardTxnsNumber, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "上次交易時間 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(ipassRead.preGenTime, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "上次交易類別 : " + Convert.ToString(ipassRead.preGenClass) + Environment.NewLine;
            F2.richTextBox1.Text += "上次交易票值/票點 : " + BitConverter.ToInt16(ipassRead.preTxnsValue, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "上次交易後票值/票點 : " + BitConverter.ToInt16(ipassRead.prePostValue, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "上次交易系統編號 : " + Convert.ToString(ipassRead.preGenSystem) + Environment.NewLine;
            F2.richTextBox1.Text += "上次交易地點編號 : " + Convert.ToString(ipassRead.preTxnsLocaion) + Environment.NewLine;
            F2.richTextBox1.Text += "上次交易機器編號 : " + BitConverter.ToInt32(ipassRead.preTxnsMachine, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "計程預收金額 : " + Convert.ToString(ipassRead.preSale) + Environment.NewLine;
            F2.richTextBox1.Text += "同步狀態 : " + Convert.ToString(ipassRead.syncStatus) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘ID : " + BitConverter.ToInt16(ipassRead.trfID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "轉換旗標 : " + Convert.ToString(ipassRead.transferFlag) + Environment.NewLine;
            F2.richTextBox1.Text += "旅遊卡天數 : " + Convert.ToString(ipassRead.travelDay) + Environment.NewLine;
            F2.richTextBox1.Text += "旅遊卡有效日 : " + BitConverter.ToInt32(ipassRead.traExpireTime, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "個人化有效日 : " + BitConverter.ToInt32(ipassRead.personalTime, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "(轉乘識別群組#1)交易時間(UNIX Time) : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(ipassRead.trfGenTime, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "(轉乘識別群組#2)本次系統代碼 : " + Convert.ToString(ipassRead.trfspeCurrentSystemID) + Environment.NewLine;
            F2.richTextBox1.Text += "(轉乘識別群組#3)前次系統代碼 : " + Convert.ToString(ipassRead.trfprePreviousSystemID) + Environment.NewLine;
            F2.richTextBox1.Text += "(轉乘識別群組#4)交易類別 : " + Convert.ToString(ipassRead.trfTxnsType) + Environment.NewLine;
            F2.richTextBox1.Text += "(轉乘識別群組#5)交易業者編號 : " + Convert.ToString(ipassRead.trfCompID) + Environment.NewLine;
            F2.richTextBox1.Text += "(轉乘識別群組#6)交易地點編號 : " + Convert.ToString(ipassRead.trfTxnsLocation) + Environment.NewLine;
            F2.richTextBox1.Text += "(轉乘識別群組#7)交易路線編號 : " + BitConverter.ToInt16(ipassRead.trfRouteID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "(轉乘識別群組#8)交易設備編號 : " + BitConverter.ToInt16(ipassRead.trfTxnsMachine, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "記名旗標 : " + Convert.ToString(ipassRead.regFlag) + Environment.NewLine;
            F2.richTextBox1.Text += "卡片狀態 : " + Convert.ToString(ipassRead.cardStatus) + Environment.NewLine;
            F2.richTextBox1.Text += "記名學生卡識別 : " + Convert.ToString(ipassRead.identifyRegStudentCard) + Environment.NewLine;
            F2.richTextBox1.Text += "定期票業者代碼 : " + Convert.ToString(ipassRead.monthlyCompID) + Environment.NewLine;
            F2.richTextBox1.Text += "evenRideBit : " + Convert.ToString(ipassRead.evenRideBit) + Environment.NewLine;
            if (Len > 128)
            {
                F2.richTextBox1.Text += "MaaSCard : " + Convert.ToString(ipassRead.MaaSCard) + Environment.NewLine;
                F2.richTextBox1.Text += "MappingType : " + Convert.ToString(ipassRead.MappingType) + Environment.NewLine;
                F2.richTextBox1.Text += "MaaSAreaCode : " + Convert.ToString(ipassRead.MaaSAreaCode) + Environment.NewLine;
                F2.richTextBox1.Text += "MaaSTransport : " + BitConverter.ToString(ipassRead.MaaSTransport, 0) + Environment.NewLine;
                F2.richTextBox1.Text += "MaaSPeriodCode : " + Convert.ToString(ipassRead.MaaSPeriodCode) + Environment.NewLine;
                F2.richTextBox1.Text += "MaaSPeriod : " + Convert.ToString(ipassRead.MaaSPeriod) + Environment.NewLine;
                F2.richTextBox1.Text += "MaaSStartDate : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(ipassRead.MaaSStartDate, 0)) + Environment.NewLine;
                F2.richTextBox1.Text += "MaaSEndDate : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(ipassRead.MaaSEndDate, 0)) + Environment.NewLine;
                /*
                F2.richTextBox1.Text += "MaaSStartEndStation : " + Convert.ToString(ipassRead.evenRideBit) + Environment.NewLine;
                F2.richTextBox1.Text += "CardNumber : " + Convert.ToString(ipassRead.CardNumber) + Environment.NewLine;
                F2.richTextBox1.Text += "EnterpriseCode : " + BitConverter.ToString(ipassRead.EnterpriseCode, 0) + Environment.NewLine;
                F2.richTextBox1.Text += "CardVersion : " + Convert.ToString(ipassRead.CardVersion) + Environment.NewLine;
                F2.richTextBox1.Text += "CardTransactionSerialNumber : " + Convert.ToString(ipassRead.CardTransactionSerialNumber) + Environment.NewLine;
                F2.richTextBox1.Text += "SixRecordIndex : " + Convert.ToString(ipassRead.SixRecordIndex) + Environment.NewLine;
                F2.richTextBox1.Text += "PersonalType : " + Convert.ToString(ipassRead.PersonalType) + Environment.NewLine;
                F2.richTextBox1.Text += "CardExpireTime : " + Convert.ToString(ipassRead.CardExpireTime) + Environment.NewLine;
                F2.richTextBox1.Text += "BankID : " + Convert.ToString(ipassRead.BankID) + Environment.NewLine;*/
            }
        }
        public void ConvertDoubleByte(double doubleVal) {
    byte	byteVal = 0;

    // Double to byte conversion can overflow.
    try {
        byteVal = System.Convert.ToByte(doubleVal);
        System.Console.WriteLine("{0} as a byte is: {1}.",
            doubleVal, byteVal);
    }
    catch (System.OverflowException) {
        System.Console.WriteLine(
            "Overflow in double-to-byte conversion.");
    }

    // Byte to double conversion cannot overflow.
    doubleVal = System.Convert.ToDouble(byteVal);
    System.Console.WriteLine("{0} as a double is: {1}.",
        byteVal, doubleVal);
}
    }
    public class icash
    {
        private ICASH_Record IcashRead;
        private ICASH_Txns_Mile IcashWrite;
        public Form2 F2;
        //=====================================================================
        public void initialization()
        {
            IcashRead.Card_No = new byte[8];
            IcashRead.Card_Balance = new byte[4];
            IcashRead.Card_TSN = new byte[4];
            IcashRead.Card_Expiration_Date = new byte[4];
            IcashRead.Points_of_Card = new byte[2];
            IcashRead.PersonalID = new byte[10];
            IcashRead.Begin_Date = new byte[4];
            IcashRead.Expiration_Date = new byte[4];
            IcashRead.Reset_Dat = new byte[4];
            IcashRead.Limit_Counts = new byte[4];
            IcashRead.TransferTime = new byte[4];
            IcashRead.TransferDiscount = new byte[2];
            IcashRead.TransferStationID = new byte[2];
            IcashRead.TransferDeviceID = new byte[4];
            IcashRead.ZoneTxnTime = new byte[4];
            IcashRead.ZoneRouteNo = new byte[2];
            IcashRead.ZoneTxnCTSN = new byte[4];
            IcashRead.ZoneTxnAmt = new byte[2];
            IcashRead.ZoneStationID = new byte[2];
            IcashRead.ZoneDeviceID = new byte[4];
            IcashRead.MileTxnTime = new byte[4];
            IcashRead.MileRouteNo = new byte[2];
            IcashRead.MileTxnCTSN = new byte[4];
            IcashRead.MileTxnAmt = new byte[2];
            IcashRead.MileStationID = new byte[2];
            IcashRead.MileDeviceID = new byte[4];
            IcashRead.Receivables = new byte[2];
            IcashRead.RideDate = new byte[4];
            //=====================================================================
            IcashWrite.DateTime = new byte[4];
            IcashWrite.Amount = new byte[2];
            IcashWrite.Receivables = new byte[2];
            IcashWrite.RouteNo = new byte[2];
            IcashWrite.Current_Station = new byte[2];
            IcashWrite.TransferDiscount = new byte[2];
            IcashWrite.DeviceID = new byte[4];
            IcashWrite.Vehicles_type = new byte[2];
            IcashWrite.Channel_Type = new byte[3];
            IcashWrite.SocialPntUsed = new byte[2];
            IcashWrite.SocialDiscount = new byte[2];
            IcashWrite.SocialResetDate = new byte[4];
            IcashWrite.RideDate = new byte[4];
            IcashWrite.TradePoint = new byte[2];
        }
        public void BytesToReadStruct(byte[] Recieved)
        {
            Array.Copy(Recieved, 5, IcashRead.Card_No, 0, 8);
            Array.Copy(Recieved, 13, IcashRead.Card_Balance, 0, 4);
            Array.Copy(Recieved, 17, IcashRead.Card_TSN, 0, 4);
            IcashRead.Card_Type = Recieved[21];
            Array.Copy(Recieved, 22, IcashRead.Card_Expiration_Date, 0, 4);
            IcashRead.Kind_of_the_ticket = Recieved[26];
            IcashRead.Area_Code = Recieved[27];
            Array.Copy(Recieved, 28, IcashRead.Points_of_Card, 0, 2);
            Array.Copy(Recieved, 30, IcashRead.PersonalID, 0, 10);
            IcashRead.ID_Code = Recieved[40];
            IcashRead.Card_Issuer = Recieved[41];
            Array.Copy(Recieved, 42, IcashRead.Begin_Date, 0, 4);
            Array.Copy(Recieved, 46, IcashRead.Expiration_Date, 0, 4);
            Array.Copy(Recieved, 50, IcashRead.Reset_Dat, 0, 4);
            Array.Copy(Recieved, 54, IcashRead.Limit_Counts, 0, 4);
            IcashRead.LastTransferCode = Recieved[58];
            IcashRead.CurrentTransferCode = Recieved[59];
            Array.Copy(Recieved, 60, IcashRead.TransferTime, 0, 4);
            IcashRead.TransferSysID = Recieved[64];
            IcashRead.TransferSpID = Recieved[65];
            IcashRead.TransferTxnType = Recieved[66];
            Array.Copy(Recieved, 67, IcashRead.TransferDiscount, 0, 2);
            Array.Copy(Recieved, 69, IcashRead.TransferStationID, 0, 2);
            Array.Copy(Recieved, 71, IcashRead.TransferDeviceID, 0, 4);
            IcashRead.ZoneTxnSysID = Recieved[75];
            IcashRead.ZoneCode = Recieved[76];
            Array.Copy(Recieved, 77, IcashRead.ZoneTxnTime, 0, 4);
            Array.Copy(Recieved, 81, IcashRead.ZoneRouteNo, 0, 2);
            Array.Copy(Recieved, 83, IcashRead.ZoneTxnCTSN, 0, 4);
            Array.Copy(Recieved, 87, IcashRead.ZoneTxnAmt, 0, 2);
            IcashRead.ZoneEntryStatus = Recieved[89];
            IcashRead.ZoneTxnType = Recieved[90];
            IcashRead.ZoneDirection = Recieved[91];
            IcashRead.ZoneSpID = Recieved[92];
            Array.Copy(Recieved, 93, IcashRead.ZoneStationID, 0, 2);
            Array.Copy(Recieved, 95, IcashRead.ZoneDeviceID, 0, 4);
            IcashRead.MileTxnSysID = Recieved[99];
            Array.Copy(Recieved, 100, IcashRead.MileTxnTime, 0, 4);
            Array.Copy(Recieved, 104, IcashRead.MileRouteNo, 0, 2);
            Array.Copy(Recieved, 106, IcashRead.MileTxnCTSN, 0, 4);
            Array.Copy(Recieved, 110, IcashRead.MileTxnAmt, 0, 2);
            IcashRead.MileEntryStatus = Recieved[112];
            IcashRead.MileTxnType = Recieved[113];
            IcashRead.MileTxnMode = Recieved[114];
            IcashRead.MileDirection = Recieved[115];
            IcashRead.MileSpID = Recieved[116];
            Array.Copy(Recieved, 117, IcashRead.MileStationID, 0, 2);
            Array.Copy(Recieved, 119, IcashRead.MileDeviceID, 0, 4);
            Array.Copy(Recieved, 123, IcashRead.Receivables, 0, 2);
            IcashRead.RideCounts = Recieved[125];
            Array.Copy(Recieved, 126, IcashRead.RideDate, 0, 4);
        }
        public byte[] GetWriteStruct()
        {
            var MemStream = new MemoryStream();
            var Binwriter = new BinaryWriter(MemStream);
            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
            IcashWrite.Trans_Mode = 0x11;
            IcashWrite.DateTime = BitConverter.GetBytes(unixTime);
            IcashWrite.Direction = 0x01;
            IcashWrite.Channel_Type[0] = 0x42;
            IcashWrite.Channel_Type[1] = 0x55;
            IcashWrite.Channel_Type[2] = 0x53;
            IcashWrite.RouteNo = IcashRead.MileRouteNo;
            IcashWrite.Current_Station = IcashRead.MileStationID;
            IcashWrite.TransferGroupCode_Before = IcashRead.LastTransferCode;
            IcashWrite.TransferGroupCode = IcashRead.CurrentTransferCode;
            IcashWrite.SysID = IcashRead.MileTxnSysID;
            IcashWrite.CompanyID = IcashRead.MileSpID;
            IcashWrite.DeviceID = IcashRead.MileDeviceID;
            IcashWrite.RideCounts = IcashRead.RideCounts;
            IcashWrite.RideDate = IcashRead.RideDate;
            if (IcashRead.MileEntryStatus == 0x00)
            {
                IcashWrite.Trans_Type = 0x00;
                IcashWrite.Trans_Status = 0x01;
            }
            else
            {

                IcashWrite.Trans_Type = 0x10;
                IcashWrite.Trans_Status = 0x00;
            }
            Binwriter.Write(IcashWrite.Trans_Mode);
            Binwriter.Write(IcashWrite.Trans_Type);
            Binwriter.Write(IcashWrite.DateTime);
            Binwriter.Write(IcashWrite.Amount);
            Binwriter.Write(IcashWrite.Receivables);
            Binwriter.Write(IcashWrite.Direction);
            Binwriter.Write(IcashWrite.Trans_Status);
            Binwriter.Write(IcashWrite.RouteNo);
            Binwriter.Write(IcashWrite.Current_Station);
            Binwriter.Write(IcashWrite.TransferGroupCode_Before);
            Binwriter.Write(IcashWrite.TransferGroupCode);
            Binwriter.Write(IcashWrite.TransferDiscount);
            Binwriter.Write(IcashWrite.SysID);
            Binwriter.Write(IcashWrite.CompanyID);
            Binwriter.Write(IcashWrite.DeviceID);
            Binwriter.Write(IcashWrite.Vehicles_type);
            Binwriter.Write(IcashWrite.Channel_Type);
            Binwriter.Write(IcashWrite.SocialPntUsed);
            Binwriter.Write(IcashWrite.SocialDiscount);
            Binwriter.Write(IcashWrite.SocialResetDate);
            Binwriter.Write(IcashWrite.RideCounts);
            Binwriter.Write(IcashWrite.RideDate);
            Binwriter.Write(IcashWrite.TradePoint);
            return MemStream.ToArray();
        }
        public int Get_Card_Balance()
        {
            return BitConverter.ToInt32(IcashRead.Card_Balance, 0);
        }
        public byte Get_Trans_Type()
        {
            return IcashWrite.Trans_Type;
        }
        public void PrintInfo()
        {
            F2.richTextBox1.Text = "卡號 : " + BitConverter.ToString(IcashRead.Card_No, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "餘額 : " + BitConverter.ToInt32(IcashRead.Card_Balance, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "票卡交易序號 : " + BitConverter.ToInt32(IcashRead.Card_TSN, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "卡片種類 : " + IcashRead.Card_Type + Environment.NewLine;
            F2.richTextBox1.Text += "卡片有效日期 : " + BitConverter.ToString(IcashRead.Card_Expiration_Date, 0, 2).Replace("-", "") + "年" + BitConverter.ToString(IcashRead.Card_Expiration_Date, 2, 1) + "月" + BitConverter.ToString(IcashRead.Card_Expiration_Date, 3, 1) + "日" + Environment.NewLine;
            F2.richTextBox1.Text += "票卡種類 : " + Convert.ToString(IcashRead.Kind_of_the_ticket) + Environment.NewLine;
            F2.richTextBox1.Text += "區碼 : " + Convert.ToString(IcashRead.Area_Code) + Environment.NewLine;
            F2.richTextBox1.Text += "票卡點數(敬老、愛陪) : " + BitConverter.ToInt16(IcashRead.Points_of_Card, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "身分證字號 : " + BitConverter.ToString(IcashRead.PersonalID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "身分識別碼 : " + Convert.ToString(IcashRead.ID_Code) + Environment.NewLine;
            F2.richTextBox1.Text += "發卡單位 : " + Convert.ToString(IcashRead.Card_Issuer) + Environment.NewLine;
            F2.richTextBox1.Text += "啟用日期 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(IcashRead.Begin_Date, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "身份有效日期 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(IcashRead.Expiration_Date, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "重置日期 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(IcashRead.Reset_Dat, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "使用上限 : " + BitConverter.ToString(IcashRead.Limit_Counts, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次轉乘代碼 : " + Convert.ToString(IcashRead.LastTransferCode) + Environment.NewLine;
            F2.richTextBox1.Text += "本次轉乘代碼 : " + Convert.ToString(IcashRead.CurrentTransferCode) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘日期 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(IcashRead.TransferTime, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘交易系統編號 : " + Convert.ToString(IcashRead.TransferSysID) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘業者代碼 : " + Convert.ToString(IcashRead.TransferSpID) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘交易類別 : " + Convert.ToString(IcashRead.TransferTxnType) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘優惠金額 : " + BitConverter.ToInt16(IcashRead.TransferDiscount, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘場站代碼 : " + BitConverter.ToInt16(IcashRead.TransferStationID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘設備編號 : " + BitConverter.ToInt32(IcashRead.TransferDeviceID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次交易系統編號 : " + Convert.ToString(IcashRead.ZoneTxnSysID) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段碼 : " + Convert.ToString(IcashRead.ZoneCode) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次交易時間 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(IcashRead.ZoneTxnTime, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次路線編號 : " + BitConverter.ToInt16(IcashRead.ZoneRouteNo, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次票卡交易序號 : " + BitConverter.ToInt32(IcashRead.ZoneTxnCTSN, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次交易金額 : " + BitConverter.ToInt16(IcashRead.ZoneTxnAmt, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次上下車狀態 : " + Convert.ToString(IcashRead.ZoneEntryStatus) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次交易類別 : " + Convert.ToString(IcashRead.ZoneTxnType) + Environment.NewLine;
            F2.richTextBox1.Text += "前次往返程註記 : " + Convert.ToString(IcashRead.ZoneDirection) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次交易業者代號 : " + Convert.ToString(IcashRead.ZoneSpID) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次交易場站代碼 : " + BitConverter.ToInt16(IcashRead.ZoneStationID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次段次設備編號 : " + BitConverter.ToInt32(IcashRead.ZoneDeviceID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程交易系統編號 : " + Convert.ToString(IcashRead.MileTxnSysID) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程交易時間 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(IcashRead.MileTxnTime, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程路線編號 : " + BitConverter.ToInt16(IcashRead.MileRouteNo, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程票卡交易序號 : " + BitConverter.ToInt32(IcashRead.MileTxnCTSN, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程交易金額 : " + BitConverter.ToInt16(IcashRead.MileTxnAmt, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次上下車狀態 : " + Convert.ToString(IcashRead.MileEntryStatus) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程交易類別 : " + Convert.ToString(IcashRead.MileTxnType) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程交易模式 : " + Convert.ToString(IcashRead.MileTxnMode) + Environment.NewLine;
            F2.richTextBox1.Text += "前次往返程註記 : " + Convert.ToString(IcashRead.MileDirection) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程交易業者代號 : " + Convert.ToString(IcashRead.MileSpID) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程交易場站代碼 : " + BitConverter.ToInt16(IcashRead.MileStationID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "前次里程設備編號 : " + BitConverter.ToInt32(IcashRead.MileDeviceID, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "上車站到終點站票價 : " + BitConverter.ToInt16(IcashRead.Receivables, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "搭乘次數 : " + Convert.ToString(IcashRead.RideCounts) + Environment.NewLine;
            F2.richTextBox1.Text += "搭乘日期 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(IcashRead.RideDate, 0)) + Environment.NewLine;
        }
    }
    public class EasyCard
    {
        private DS_CSC_READ_FOR_MILAGE_BV_7B EasyCardRead;
        private DS_CSC_MILAGE_DEDUCT_IN_V3 EasyCardWrite;
        public Form2 F2;
        //=====================================================================
        public void initialization()
        {
            EasyCardRead.cmd_manufacture_serial_number = new byte[4];
            EasyCardRead.cid_begin_time = new byte[4];
            EasyCardRead.cid_expiry_time = new byte[4];
            EasyCardRead.gsp_autopay_value = new byte[2];
            EasyCardRead.gsp_max_ev = new byte[2];
            EasyCardRead.gsp_max_deduct_value = new byte[2];
            EasyCardRead.gsp_profile_expiry_date = new byte[2];
            EasyCardRead.cpd_social_security_code = new byte[6];
            EasyCardRead.cpd_deposit = new byte[2];
            EasyCardRead.ev = new byte[2];
            EasyCardRead.tsd_transaction_sequence_number = new byte[2];
            EasyCardRead.tsd_loyalty_points = new byte[2];
            EasyCardRead.tsd_add_value_accumulated_points = new byte[2];
            EasyCardRead.urt_transfer_group_code_new = new byte[2];
            EasyCardRead.urt_transaction_date_and_time = new byte[4];
            EasyCardRead.urt_transfer_discount = new byte[2];
            EasyCardRead.urt_ev_afetr_transfer = new byte[2];
            EasyCardRead.urt_transaction_equipment_id = new byte[4];
            EasyCardRead.busfix_first_possible_utilization_date = new byte[2];
            EasyCardRead.busfix_last_possible_utilization_date = new byte[2];
            EasyCardRead.busfix_vip_points = new byte[2];
            EasyCardRead.busvar_date_of_first_transaction = new byte[2];
            EasyCardRead.busvar_device_serial_number = new byte[2];
            EasyCardRead.busvar_transaction_date_and_time = new byte[4];
            EasyCardRead.busvar_value_of_transaction = new byte[2];
            EasyCardRead.busvar_free_transaction_date_and_time = new byte[4];
            EasyCardRead.busvar_accumulated_free_rides2 = new byte[2];
            EasyCardRead.busvar_free_transaction_date_and_time2 = new byte[2];
            EasyCardRead.var_transaction_date_time = new byte[4];
            EasyCardRead.var_value_of_transaction = new byte[2];
            EasyCardRead.var_ev_afetr_transaction = new byte[2];
            EasyCardRead.var_transaction_device_id = new byte[4];
            //===========================================
            EasyCardWrite.utr_transaction_date_time = new byte[4];
            EasyCardWrite.cmd_manufacture_serial_number = new byte[4];
            EasyCardWrite.deducted_value = new byte[2];
            EasyCardWrite.urt_transfer_date_time = new byte[4];
            EasyCardWrite.urt_transfer_discount = new byte[2];
            EasyCardWrite.urt_transfer_group_code_new = new byte[2];
            EasyCardWrite.busvar_date_of_first_transaction = new byte[2];
            EasyCardWrite.busvar_line2_number = new byte[2];
        }
        public void BytesToReadStruct(byte[] Recieved)
        {
            Array.Copy(Recieved, 5, EasyCardRead.cmd_manufacture_serial_number, 0, 4);
            EasyCardRead.cid_issuer_code = Recieved[9];
            Array.Copy(Recieved, 10, EasyCardRead.cid_begin_time, 0, 4);
            Array.Copy(Recieved, 14, EasyCardRead.cid_expiry_time, 0, 4);
            EasyCardRead.cid_status = Recieved[18];
            EasyCardRead.gsp_autopay_flag = Recieved[19];
            Array.Copy(Recieved, 20, EasyCardRead.gsp_autopay_value, 0, 2);
            Array.Copy(Recieved, 22, EasyCardRead.gsp_max_ev, 0, 2);
            Array.Copy(Recieved, 24, EasyCardRead.gsp_max_deduct_value, 0, 2);
            EasyCardRead.gsp_personal_profile = Recieved[26];
            Array.Copy(Recieved, 27, EasyCardRead.gsp_profile_expiry_date, 0, 2);
            EasyCardRead.gsp_area_code = Recieved[29];
            EasyCardRead.gsp_bank_code = Recieved[30];
            EasyCardRead.area_auth_flag = Recieved[31];
            EasyCardRead.special_ticket_type = Recieved[32];
            Array.Copy(Recieved, 33, EasyCardRead.cpd_social_security_code, 0, 6);
            Array.Copy(Recieved, 39, EasyCardRead.cpd_deposit, 0, 2);
            Array.Copy(Recieved, 41, EasyCardRead.ev, 0, 2);
            Array.Copy(Recieved, 43, EasyCardRead.tsd_transaction_sequence_number, 0, 2);
            Array.Copy(Recieved, 45, EasyCardRead.tsd_loyalty_points, 0, 2);
            Array.Copy(Recieved, 47, EasyCardRead.tsd_add_value_accumulated_points, 0, 2);
            EasyCardRead.urt_transaction_sequence_number_LSB = Recieved[49];
            Array.Copy(Recieved, 50, EasyCardRead.urt_transfer_group_code_new, 0, 2);
            Array.Copy(Recieved, 52, EasyCardRead.urt_transaction_date_and_time, 0, 4);
            EasyCardRead.urt_transaction_type = Recieved[56];
            Array.Copy(Recieved, 57, EasyCardRead.urt_transfer_discount, 0, 2);
            Array.Copy(Recieved, 59, EasyCardRead.urt_ev_afetr_transfer, 0, 2);
            EasyCardRead.urt_transfer_group_code = Recieved[61];
            EasyCardRead.urt_transaction_location_code = Recieved[62];
            Array.Copy(Recieved, 63, EasyCardRead.urt_transaction_equipment_id, 0, 4);
            EasyCardRead.busfix_fare_product_company = Recieved[67];
            EasyCardRead.busfix_fare_product_kind = Recieved[68];
            EasyCardRead.busfix_fare_product_type = Recieved[69];
            Array.Copy(Recieved, 70, EasyCardRead.busfix_first_possible_utilization_date, 0, 2);
            Array.Copy(Recieved, 72, EasyCardRead.busfix_last_possible_utilization_date, 0, 2);
            EasyCardRead.busfix_number_of_use = Recieved[74];
            EasyCardRead.busfix_duration_of_use = Recieved[75];
            EasyCardRead.busfix_authorized_lines = Recieved[76];
            EasyCardRead.busfix_authorized_groups = Recieved[77];
            EasyCardRead.busfix_stop1_number = Recieved[78];
            EasyCardRead.busfix_stop2_number = Recieved[79];
            Array.Copy(Recieved, 80, EasyCardRead.busfix_vip_points, 0, 2);
            EasyCardRead.busvar_current_used_number = Recieved[82];
            Array.Copy(Recieved, 83, EasyCardRead.busvar_date_of_first_transaction, 0, 2);
            EasyCardRead.busvar_milage_forbiddance_flag = Recieved[85];
            EasyCardRead.busvar_special_permission = Recieved[86];
            EasyCardRead.busvar_travel_to_or_from = Recieved[87];
            EasyCardRead.busvar_get_on_or_off = Recieved[88];
            EasyCardRead.busvar_company_number = Recieved[89];
            Array.Copy(Recieved, 90, EasyCardRead.busvar_device_serial_number, 0, 2);
            EasyCardRead.busvar_line_number = Recieved[92];
            Array.Copy(Recieved, 93, EasyCardRead.busvar_transaction_date_and_time, 0, 4);
            EasyCardRead.busvar_stop_number = Recieved[97];
            Array.Copy(Recieved, 98, EasyCardRead.busvar_value_of_transaction, 0, 2);
            EasyCardRead.busvar_travel_mode = Recieved[100];
            EasyCardRead.busvar_vip_accumulated_points1 = Recieved[101];
            EasyCardRead.busvar_vip_accumulated_points2 = Recieved[102];
            EasyCardRead.busvar_accumulated_free_rides = Recieved[103];
            Array.Copy(Recieved, 104, EasyCardRead.busvar_free_transaction_date_and_time, 0, 4);
            Array.Copy(Recieved, 108, EasyCardRead.busvar_accumulated_free_rides2, 0, 2);
            Array.Copy(Recieved, 110, EasyCardRead.busvar_free_transaction_date_and_time2, 0, 2);
            EasyCardRead.var_transaction_number_lsb = Recieved[112];
            Array.Copy(Recieved, 113, EasyCardRead.var_transaction_date_time, 0, 4);
            EasyCardRead.var_transaction_type = Recieved[117];
            Array.Copy(Recieved, 118, EasyCardRead.var_value_of_transaction, 0, 2);
            Array.Copy(Recieved, 120, EasyCardRead.var_ev_afetr_transaction, 0, 2);
            EasyCardRead.var_operator_code = Recieved[122];
            EasyCardRead.var_transaction_location_code = Recieved[123];
            Array.Copy(Recieved, 124, EasyCardRead.var_transaction_device_id, 0, 4);
        }
        public byte[] GetWriteStruct()
        {
            var MemStream = new MemoryStream();
            var Binwriter = new BinaryWriter(MemStream);
            int unixTime = Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds() + 28800);
            EasyCardWrite.message_type = 0x01;
            EasyCardWrite.utr_transaction_date_time = BitConverter.GetBytes(unixTime);
            EasyCardWrite.cmd_manufacture_serial_number = EasyCardRead.cmd_manufacture_serial_number;
            EasyCardWrite.urt_transfer_date_time = EasyCardRead.urt_transaction_date_and_time;
            EasyCardWrite.urt_transfer_discount = EasyCardRead.urt_transfer_discount;
            EasyCardWrite.urt_transfer_group_code = EasyCardRead.urt_transfer_group_code;
            EasyCardWrite.urt_transfer_group_code_new = EasyCardRead.urt_transfer_group_code_new;
            EasyCardWrite.busvar_current_used_number = EasyCardRead.busvar_current_used_number;
            EasyCardWrite.busvar_date_of_first_transaction = EasyCardRead.busvar_date_of_first_transaction;
            EasyCardWrite.busvar_travel_to_or_from = EasyCardRead.busvar_travel_to_or_from;
            EasyCardWrite.busvar_line1_number = EasyCardRead.busvar_line_number;
            EasyCardWrite.busvar_line2_number[0] = EasyCardRead.busvar_line_number;
            EasyCardWrite.busvar_stop_number = EasyCardRead.busvar_stop_number;
            EasyCardWrite.busvar_travel_mode = 0x01;
            EasyCardWrite.busvar_vip_accumulated_points1 = EasyCardRead.busvar_vip_accumulated_points1;
            EasyCardWrite.busvar_vip_accumulated_points2 = EasyCardRead.busvar_vip_accumulated_points2;
            if (EasyCardRead.busvar_get_on_or_off == 0x00)
            {
                EasyCardWrite.busvar_get_on_or_off = 0x15;
                EasyCardWrite.busvar_transaction_type = 0x00;
            }
            else
            {
                EasyCardWrite.busvar_get_on_or_off = 0x14;
                EasyCardWrite.busvar_transaction_type = 0x10;
            }
            Binwriter.Write(EasyCardWrite.message_type);
            Binwriter.Write(EasyCardWrite.utr_transaction_date_time);
            Binwriter.Write(EasyCardWrite.cmd_manufacture_serial_number);
            Binwriter.Write(EasyCardWrite.deducted_value);
            Binwriter.Write(EasyCardWrite.urt_transfer_date_time);
            Binwriter.Write(EasyCardWrite.urt_transfer_discount);
            Binwriter.Write(EasyCardWrite.urt_transfer_group_code);
            Binwriter.Write(EasyCardWrite.urt_transfer_group_code_new);
            Binwriter.Write(EasyCardWrite.busvar_current_used_number);
            Binwriter.Write(EasyCardWrite.busvar_date_of_first_transaction);
            Binwriter.Write(EasyCardWrite.busvar_milage_forbiddance_flag);
            Binwriter.Write(EasyCardWrite.busvar_special_permission);
            Binwriter.Write(EasyCardWrite.busvar_travel_to_or_from);
            Binwriter.Write(EasyCardWrite.busvar_get_on_or_off);
            Binwriter.Write(EasyCardWrite.busvar_line1_number);
            Binwriter.Write(EasyCardWrite.busvar_line2_number);
            Binwriter.Write(EasyCardWrite.busvar_stop_number);
            Binwriter.Write(EasyCardWrite.busvar_travel_mode);
            Binwriter.Write(EasyCardWrite.busvar_transaction_type);
            Binwriter.Write(EasyCardWrite.busvar_vip_accumulated_points1);
            Binwriter.Write(EasyCardWrite.busvar_vip_accumulated_points2);
            Binwriter.Write(EasyCardWrite.busvar_remaining_rides);
            return MemStream.ToArray();
        }
        public int Get_ev()
        {
            return BitConverter.ToInt16(EasyCardRead.ev, 0);
        }
        public void PrintInfo()
        {
            F2.richTextBox1.Text = "卡號 : " + BitConverter.ToString(EasyCardRead.cmd_manufacture_serial_number) + Environment.NewLine;
            F2.richTextBox1.Text += "發卡公司 : " + String.Format("{0:X2}", EasyCardRead.cid_issuer_code) + Environment.NewLine;
            F2.richTextBox1.Text += "票卡起始日期 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(EasyCardRead.cid_begin_time, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "票卡到期日期 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(EasyCardRead.cid_expiry_time, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "票卡狀態 : " + String.Format("{0:X2}", EasyCardRead.cid_status) + Environment.NewLine;
            F2.richTextBox1.Text += "自動加值授權認證旗標 : " + String.Format("{0:X2}", EasyCardRead.gsp_autopay_flag) + Environment.NewLine;
            F2.richTextBox1.Text += "自動加值金額 : " + BitConverter.ToUInt16(EasyCardRead.gsp_autopay_value, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "票卡最大額度上限 : " + BitConverter.ToUInt16(EasyCardRead.gsp_max_ev, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "票卡最大扣款金額 : " + BitConverter.ToUInt16(EasyCardRead.gsp_max_deduct_value, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "票卡身份別 : " + String.Format("{0:X2}", EasyCardRead.gsp_personal_profile) + Environment.NewLine;
            F2.richTextBox1.Text += "身份到期日 : " + new DateTime(1980, 1, 1, 0, 0, 0).AddYears(BitConverter.ToUInt16(EasyCardRead.gsp_profile_expiry_date, 0) / 512).AddMonths(BitConverter.ToUInt16(EasyCardRead.gsp_profile_expiry_date, 0) % 512 / 32 - 1).AddDays(BitConverter.ToUInt16(EasyCardRead.gsp_profile_expiry_date, 0) % 32 - 1).ToString("yyyy/MM/dd") + Environment.NewLine;
            F2.richTextBox1.Text += "區碼 : " + String.Format("{0:X2}", EasyCardRead.gsp_area_code) + Environment.NewLine;
            F2.richTextBox1.Text += "銀行代碼 : " + String.Format("{0:X2}", EasyCardRead.gsp_bank_code) + Environment.NewLine;
            F2.richTextBox1.Text += "地區認證旗標 : " + String.Format("{0,8}", Convert.ToString(EasyCardRead.area_auth_flag, 2)).Replace(" ", "0") + Environment.NewLine;
            F2.richTextBox1.Text += "特殊票票別 : " + String.Format("{0:X2}", EasyCardRead.special_ticket_type) + Environment.NewLine;
            F2.richTextBox1.Text += "身份證號碼 : " + BitConverter.ToString(EasyCardRead.cpd_social_security_code) + Environment.NewLine;
            F2.richTextBox1.Text += "押金 : " + BitConverter.ToUInt16(EasyCardRead.cpd_deposit, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "電子錢包額度(餘額) : " + BitConverter.ToInt16(EasyCardRead.ev, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "交易序號 : " + BitConverter.ToUInt16(EasyCardRead.tsd_transaction_sequence_number, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "忠誠累積點數 : " + BitConverter.ToUInt16(EasyCardRead.tsd_loyalty_points, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "加值累積點數 : " + BitConverter.ToUInt16(EasyCardRead.tsd_add_value_accumulated_points, 0) + Environment.NewLine;
            //====================================================================
            F2.richTextBox1.Text += "轉乘交易序號末碼 : " + String.Format("{0:X2}", EasyCardRead.urt_transaction_sequence_number_LSB) + Environment.NewLine;
            F2.richTextBox1.Text += "新轉乘群組代碼 : " + BitConverter.ToString(EasyCardRead.urt_transfer_group_code_new) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘交易時間 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(EasyCardRead.urt_transaction_date_and_time, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘方式 : " + String.Format("{0:X2}", EasyCardRead.busvar_stop_number) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘金額 : " + BitConverter.ToUInt16(EasyCardRead.urt_transfer_discount, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘後票卡餘額 : " + BitConverter.ToInt16(EasyCardRead.urt_ev_afetr_transfer, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘群組代碼 : " + String.Format("{0:X2}", EasyCardRead.urt_transfer_group_code) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘交易場站代碼 : " + String.Format("{0:X2}", EasyCardRead.urt_transaction_location_code) + Environment.NewLine;
            F2.richTextBox1.Text += "轉乘交易設備編號 : " + BitConverter.ToString(EasyCardRead.urt_transaction_equipment_id) + Environment.NewLine;
            //====================================================================
            F2.richTextBox1.Text += "特種票交易公司代碼 : " + String.Format("{0:X2}", EasyCardRead.busfix_fare_product_company) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票分類 : " + String.Format("{0:X2}", EasyCardRead.busfix_fare_product_kind) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票票種 : " + String.Format("{0:X2}", EasyCardRead.busfix_fare_product_type) + Environment.NewLine;
            if (BitConverter.ToUInt16(EasyCardRead.busfix_first_possible_utilization_date, 0) > 0)
                F2.richTextBox1.Text += "特種票起始日 : " + new DateTime(1980, 1, 1, 0, 0, 0).AddYears(BitConverter.ToUInt16(EasyCardRead.busfix_first_possible_utilization_date, 0) / 512).AddMonths(BitConverter.ToUInt16(EasyCardRead.busfix_first_possible_utilization_date, 0) % 512 / 32 - 1).AddDays(BitConverter.ToUInt16(EasyCardRead.busfix_first_possible_utilization_date, 0) % 32 - 1).ToString("yyyy/MM/dd") + Environment.NewLine;
            else
                F2.richTextBox1.Text += "特種票起始日 : " + BitConverter.ToUInt16(EasyCardRead.busfix_first_possible_utilization_date, 0) + Environment.NewLine;
            if (BitConverter.ToUInt16(EasyCardRead.busfix_last_possible_utilization_date, 0) > 0)
                F2.richTextBox1.Text += "特種票到期日 : " + new DateTime(1980, 1, 1, 0, 0, 0).AddYears(BitConverter.ToUInt16(EasyCardRead.busfix_last_possible_utilization_date, 0) / 512).AddMonths(BitConverter.ToUInt16(EasyCardRead.busfix_last_possible_utilization_date, 0) % 512 / 32 - 1).AddDays(BitConverter.ToUInt16(EasyCardRead.busfix_last_possible_utilization_date, 0) % 32 - 1).ToString("yyyy/MM/dd") + Environment.NewLine;
            else
                F2.richTextBox1.Text += "特種票到期日 : " + BitConverter.ToUInt16(EasyCardRead.busfix_last_possible_utilization_date, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票可使用次數 : " + String.Format("{0:X2}", EasyCardRead.busfix_number_of_use) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票期限 : " + String.Format("{0:X2}", EasyCardRead.busfix_duration_of_use) + Environment.NewLine;
            F2.richTextBox1.Text += "卡特種票可用路線代碼號 : " + String.Format("{0:X2}", EasyCardRead.busfix_authorized_lines) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票可用路線群組 : " + String.Format("{0:X2}", EasyCardRead.busfix_authorized_groups) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票起迄站代碼1 : " + String.Format("{0:X2}", EasyCardRead.busfix_stop1_number) + Environment.NewLine;
            F2.richTextBox1.Text += "特種票起迄站代碼2 : " + String.Format("{0:X2}", EasyCardRead.busfix_stop2_number) + Environment.NewLine;
            F2.richTextBox1.Text += "VIP 票累積儲值點數 : " + BitConverter.ToUInt16(EasyCardRead.busfix_vip_points, 0) + Environment.NewLine;
            //====================================================================
            F2.richTextBox1.Text += "特種票已用次數 : " + String.Format("{0:X2}", EasyCardRead.busvar_current_used_number) + Environment.NewLine;
            if (BitConverter.ToUInt16(EasyCardRead.busfix_first_possible_utilization_date, 0) > 0)
                F2.richTextBox1.Text += "特種票首次交易日期 : " + new DateTime(1980, 1, 1, 0, 0, 0).AddYears(BitConverter.ToUInt16(EasyCardRead.busvar_date_of_first_transaction, 0) / 512).AddMonths(BitConverter.ToUInt16(EasyCardRead.busvar_date_of_first_transaction, 0) % 512 / 32 - 1).AddDays(BitConverter.ToUInt16(EasyCardRead.busvar_date_of_first_transaction, 0) % 32 - 1).ToString("yyyy/MM/dd") + Environment.NewLine;
            else
                F2.richTextBox1.Text += "特種票首次交易日期 : " + BitConverter.ToUInt16(EasyCardRead.busvar_date_of_first_transaction, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "里程計費系統禁用旗標 : " + String.Format("{0:X2}", EasyCardRead.busvar_milage_forbiddance_flag) + Environment.NewLine;
            F2.richTextBox1.Text += "特許權 : " + String.Format("{0:X2}", EasyCardRead.busvar_special_permission) + Environment.NewLine;
            F2.richTextBox1.Text += "往返程註記 : " + String.Format("{0:X2}", EasyCardRead.busvar_travel_to_or_from) + Environment.NewLine;
            F2.richTextBox1.Text += "上下車狀態 : " + String.Format("{0:X2}", EasyCardRead.busvar_get_on_or_off) + Environment.NewLine;
            F2.richTextBox1.Text += "交易公司代碼 : " + String.Format("{0:X2}", EasyCardRead.busvar_company_number) + Environment.NewLine;
            F2.richTextBox1.Text += "設備次編號 : " + BitConverter.ToUInt16(EasyCardRead.busvar_device_serial_number, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "路線代碼 : " + String.Format("{0:X2}", EasyCardRead.busvar_line_number) + Environment.NewLine;
            F2.richTextBox1.Text += "交易時間 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(EasyCardRead.busvar_transaction_date_and_time, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "站牌代碼 : " + String.Format("{0:X2}", EasyCardRead.busvar_company_number) + Environment.NewLine;
            F2.richTextBox1.Text += "交易金額 : " + BitConverter.ToUInt16(EasyCardRead.busvar_value_of_transaction, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "搭乘模式 : " + String.Format("{0:X2}", EasyCardRead.busvar_travel_mode) + Environment.NewLine;
            F2.richTextBox1.Text += "搭乘次數旗標 &VIP 票累積已用點數 : " + String.Format("{0:X2}", EasyCardRead.busvar_vip_accumulated_points1) + Environment.NewLine;
            F2.richTextBox1.Text += "VIP 票累積已用點數2 : " + String.Format("{0:X2}", EasyCardRead.busvar_vip_accumulated_points2) + Environment.NewLine;
            F2.richTextBox1.Text += "累積優惠點數 : " + String.Format("{0:X2}", EasyCardRead.busvar_accumulated_free_rides) + Environment.NewLine;
            F2.richTextBox1.Text += "優惠點數交易時間 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(EasyCardRead.busvar_free_transaction_date_and_time, 0)) + Environment.NewLine;
            F2.richTextBox1.Text += "累積優惠點數2 : " + BitConverter.ToInt16(EasyCardRead.busvar_accumulated_free_rides2, 0) + Environment.NewLine;
            if (BitConverter.ToUInt16(EasyCardRead.busvar_free_transaction_date_and_time2, 0) > 0)
                F2.richTextBox1.Text += "累積優惠點數2 交易日期 : " + new DateTime(1980, 1, 1, 0, 0, 0).AddYears(BitConverter.ToUInt16(EasyCardRead.busvar_free_transaction_date_and_time2, 0) / 512).AddMonths(BitConverter.ToUInt16(EasyCardRead.busvar_free_transaction_date_and_time2, 0) % 512 / 32 - 1).AddDays(BitConverter.ToUInt16(EasyCardRead.busvar_free_transaction_date_and_time2, 0) % 32 - 1).ToString("yyyy/MM/dd") + Environment.NewLine;
            else
                F2.richTextBox1.Text += "累積優惠點數2 交易日期 : " + BitConverter.ToUInt16(EasyCardRead.busvar_free_transaction_date_and_time2, 0) + Environment.NewLine;
            //====================================================================
            F2.richTextBox1.Text += "交易序號末碼 : " + String.Format("{0:X2}", EasyCardRead.var_transaction_number_lsb) + Environment.NewLine;
            if (BitConverter.ToUInt16(EasyCardRead.var_transaction_date_time, 0) > 0)
                F2.richTextBox1.Text += "加值時間 : " + new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(EasyCardRead.var_transaction_date_time, 0)) + Environment.NewLine;
            else
                F2.richTextBox1.Text += "加值時間 : " + BitConverter.ToInt32(EasyCardRead.var_transaction_date_time, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "加值方式代碼 : " + String.Format("{0:X2}", EasyCardRead.var_transaction_type) + Environment.NewLine;
            F2.richTextBox1.Text += "加值金額 : " + BitConverter.ToInt16(EasyCardRead.var_value_of_transaction, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "加值後餘額 : " + BitConverter.ToInt16(EasyCardRead.var_ev_afetr_transaction, 0) + Environment.NewLine;
            F2.richTextBox1.Text += "交易公司代碼 : " + String.Format("{0:X2}", EasyCardRead.var_operator_code) + Environment.NewLine;
            F2.richTextBox1.Text += "交易場站代碼 : " + String.Format("{0:X2}", EasyCardRead.var_transaction_location_code) + Environment.NewLine;
            F2.richTextBox1.Text += "交易設備編號 : " + BitConverter.ToString(EasyCardRead.var_transaction_device_id) + Environment.NewLine;
        }

    }
    /*函式擴充處*/

}
