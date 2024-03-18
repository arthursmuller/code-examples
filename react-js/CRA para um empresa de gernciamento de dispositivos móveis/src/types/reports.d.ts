import { DeviceType } from "./device"
import { ID } from "./util"

export type SiteType = {
  id?: ID;
  domain: string;
  accessedAt: string;
  device?: DeviceType;
}

export type ReportSiteDateType = {
  date: string;
  items: Array<SiteType & {markColor?: string;} >;
}

export type ReportSiteUrlType = SiteType
