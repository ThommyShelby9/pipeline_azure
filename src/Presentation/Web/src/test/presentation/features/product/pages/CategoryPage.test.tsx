import { describe, it, expect, vi } from 'vitest';
import { renderWithProviders, screen } from '@/test/test-utils';
import CategoryPage from '@/presentation/features/product/pages/CategoryPage';

vi.mock('@/presentation/features/product/hooks/useProducts', () => ({
  useProducts: () => ({
    data: [
      { id: 1, title: 'P1', price: 10, description: '', category: 'electronics', image: '', rating: { rate: 4, count: 1 } },
      { id: 2, title: 'P2', price: 20, description: '', category: 'jewelery', image: '', rating: { rate: 4, count: 1 } },
    ],
    isLoading: false,
    isError: false,
  }),
}));

describe('CategoryPage', () => {
  it('renders categories header and cards', () => {
    renderWithProviders(<CategoryPage />);

    expect(screen.getByRole('heading', { name: /categories/i })).toBeInTheDocument();
    expect(screen.getByText(/electronics/i)).toBeInTheDocument();
    expect(screen.getByText(/jewelery/i)).toBeInTheDocument();
  });
});


