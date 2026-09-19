import { Component, OnInit, ChangeDetectorRef, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Company } from '../../../domain/models/company';
import { CompanyUiService } from '../../services/company-ui.service';

@Component({
  selector: 'app-company-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})
export class CompanyListComponent implements OnInit {
  companies: Company[] = [];
  loading = true;
  error: string | null = null;

  private service = inject(CompanyUiService);
  private cdr = inject(ChangeDetectorRef);

  async ngOnInit(): Promise<void> {
    this.loading = true;
    this.error = null;
    try {
      console.log('[CompanyList] fetching companies...');
      const data = await this.service.getAll();
      console.log('[CompanyList] received companies:', data);
      
      this.companies = data;
    } catch (err) {
      console.error('[CompanyList] error fetching companies', err);
      this.error = (err as any)?.message ?? String(err);
    } finally {
      this.loading = false;
      // Force Angular to re-evaluate the template and update the DOM
      this.cdr.detectChanges();
    }
  }
}
