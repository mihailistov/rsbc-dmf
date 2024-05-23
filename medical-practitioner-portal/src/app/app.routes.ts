import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CaseDetailsComponent } from './case-details/case-details.component';
import { AccountComponent } from './account/account.component';
import { GetHelpComponent } from './get-help/get-help.component';
import { CaseSubmissionsComponent } from '../case-submissions/case-submissions.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    component: DashboardComponent,
  },
  { path: 'caseDetails/:caseId', component: CaseDetailsComponent },
  { path: 'account', component: AccountComponent },
  { path: 'getHelp', component: GetHelpComponent },
  { path: 'caseSubmissions', component: CaseSubmissionsComponent },
];
