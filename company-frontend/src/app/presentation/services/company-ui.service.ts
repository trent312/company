import { Injectable } from '@angular/core';
import { Company } from '../../domain/models/company';
import { ApiCompanyRepository } from '../../infrastructure/repositories/api-company.repository';

@Injectable({ providedIn: 'root' })
export class CompanyUiService {
  constructor(private repo: ApiCompanyRepository) {}

  async getAll(): Promise<Company[]> {
    return this.repo.getAll();
  }

  async search(q?: string): Promise<Company[]> {
    return this.repo.search(q);
  }
}
