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

    // Default language is English, so check for English text
    expect(screen.getByText(/your cart is empty/i)).toBeInTheDocument();
    expect(screen.getByRole('link', { name: /start shopping/i })).toBeInTheDocument();
    expect(
      screen.getByRole('link', { name: /browse categories/i })
    ).toBeInTheDocument();
  });
});


