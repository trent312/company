import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { Company } from '../../domain/models/company';
import { CompanyRepository } from '../../application/ports/company-repository.port';
import { environment } from '../../../environment';

@Injectable({ providedIn: 'root' })
export class ApiCompanyRepository implements CompanyRepository {
  private base = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  async save(company: Company): Promise<number> {
    // request full response to inspect Location header
    const resp$ = this.http.post<Company>(this.base, company, { observe: 'response' });
    const resp = await firstValueFrom(resp$) as HttpResponse<Company>;

    // try Location header
    const location = resp.headers.get('location') || resp.headers.get('Location');
    if (location) {
      const parts = location.split('/');
      const last = parts[parts.length - 1];
      const id = parseInt(last, 10);
      if (!isNaN(id)) return id;
    }

    // fallback to body id
    const body = resp.body as any;
    if (body && (body.id || body.id === 0)) return body.id as number;

    return -1;
  }

  async get(id: number): Promise<Company | null> {
    try {
      const obs$ = this.http.get<Company>(`${this.base}/${id}`);
      const company = await firstValueFrom(obs$);
      return company;
    } catch (err: any) {
      if (err?.status === 404) return null;
      throw err;
    }
  }

  async getAll(): Promise<Company[]> {
    const obs$ = this.http.get<Company[]>(this.base);
    return await firstValueFrom(obs$);
  }

  async search(query?: string): Promise<Company[]> {
    let params = new HttpParams();
    if (query) params = params.set('q', query);
    const obs$ = this.http.get<Company[]>(this.base, { params });
    return await firstValueFrom(obs$);
  }
}
