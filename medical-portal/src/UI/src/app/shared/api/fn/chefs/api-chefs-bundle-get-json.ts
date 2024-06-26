/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { ChefsBundle } from '../../models/chefs-bundle';

export interface ApiChefsBundleGet$Json$Params {
  caseId: string;
}

export function apiChefsBundleGet$Json(http: HttpClient, rootUrl: string, params: ApiChefsBundleGet$Json$Params, context?: HttpContext): Observable<StrictHttpResponse<ChefsBundle>> {
  const rb = new RequestBuilder(rootUrl, apiChefsBundleGet$Json.PATH, 'get');
  if (params) {
    rb.query('caseId', params.caseId, {});
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<ChefsBundle>;
    })
  );
}

apiChefsBundleGet$Json.PATH = '/api/Chefs/bundle';
