/* eslint-disable react-refresh/only-export-components */
import { ReactElement } from 'react';
import { render, RenderOptions } from '@testing-library/react';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { I18nextProvider } from 'react-i18next';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { configureStore, combineReducers } from '@reduxjs/toolkit';
import i18n from '@/i18n/config';
import authReducer from '@/presentation/store/slices/authSlice';
import cartReducer from '@/presentation/store/slices/cartSlice';
import uiReducer from '@/presentation/store/slices/uiSlice';
import preferencesReducer from '@/presentation/store/slices/preferencesSlice';
import productsReducer from '@/presentation/store/slices/productsSlice';

// Create a test store without persistence
export function createTestStore(preloadedState?: any) {
  const rootReducer = combineReducers({
    auth: authReducer,
    cart: cartReducer,
    ui: uiReducer,
    preferences: preferencesReducer,
    products: productsReducer,
  });

  return configureStore({
    reducer: rootReducer,
    preloadedState,
    middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware({
        serializableCheck: false,
      }),
  });
}

// Create a test QueryClient
export function createTestQueryClient() {
  return new QueryClient({
    defaultOptions: {
      queries: {
        retry: false,
        gcTime: 0,
      },
      mutations: {
        retry: false,
      },
    },
    logger: {
      log: () => {},
      warn: () => {},
      error: () => {},
    },
  });
}

interface AllTheProvidersProps {
  children: React.ReactNode;
  store?: ReturnType<typeof createTestStore>;
  queryClient?: QueryClient;
}

function AllTheProviders({ children, store, queryClient }: AllTheProvidersProps) {
  const testStore = store || createTestStore();
  const testQueryClient = queryClient || createTestQueryClient();

  return (
    <Provider store={testStore}>
      <QueryClientProvider client={testQueryClient}>
        <BrowserRouter>
          <I18nextProvider i18n={i18n}>
            {children}
          </I18nextProvider>
        </BrowserRouter>
      </QueryClientProvider>
    </Provider>
  );
}

interface CustomRenderOptions extends Omit<RenderOptions, 'wrapper'> {
  preloadedState?: any;
  store?: ReturnType<typeof createTestStore>;
  queryClient?: QueryClient;
}

export function renderWithProviders(
  ui: ReactElement,
  {
    preloadedState,
    store = createTestStore(preloadedState),
    queryClient,
    ...renderOptions
  }: CustomRenderOptions = {}
) {
  function Wrapper({ children }: { children: React.ReactNode }) {
    return (
      <AllTheProviders store={store} queryClient={queryClient}>
        {children}
      </AllTheProviders>
    );
  }

  return {
    store,
    ...render(ui, { wrapper: Wrapper, ...renderOptions }),
  };
}

// Re-export everything from React Testing Library
export * from '@testing-library/react';
