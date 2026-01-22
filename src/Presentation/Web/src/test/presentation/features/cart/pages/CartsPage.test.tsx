import { describe, it, expect, vi } from 'vitest';
import { renderWithProviders, screen } from '@/test/test-utils';
import CartsPage from '@/presentation/features/cart/pages/CartsPage';

// Mock useCart hook
vi.mock('@/presentation/features/cart/hooks/useCart', () => ({
  useCart: () => ({
    cartItems: [],
    isLoading: false,
    updateCartItem: vi.fn(),
    removeFromCart: vi.fn(),
    clearCart: vi.fn(),
    totalItems: 0,
    totalPrice: 0,
  }),
}));

describe('CartsPage', () => {
  it('renders empty cart state with i18n texts', () => {
    renderWithProviders(<CartsPage />);

    expect(screen.getByText(/sepetiniz boş/i)).toBeInTheDocument();
    expect(screen.getByRole('button', { name: /alışverişe başla/i })).toBeInTheDocument();
    expect(
      screen.getByRole('link', { name: /alışverişe devam et/i })
    ).toBeInTheDocument();
  });
});


