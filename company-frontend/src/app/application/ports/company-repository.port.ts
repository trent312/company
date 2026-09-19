import { Company } from '../../domain/models/company';

export interface CompanyRepository {
  save(company: Company): Promise<number>;
  get(id: number): Promise<Company | null>;
  getAll(): Promise<Company[]>;
  search(query?: string): Promise<Company[]>;
}
